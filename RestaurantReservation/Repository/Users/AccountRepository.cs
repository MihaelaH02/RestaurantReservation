using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.RegisterDTO;
using RestaurantReservation.Models.Nomenclatures;
using RestaurantReservation.Models.Users;
using System.Security.Principal;
using System.Text.Json;

namespace RestaurantReservation.Repository.Users
{
    /// <summary> Клас за обработка на акаунти към базата данни </summary>
    public class AccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Accounts> _passwordHasher;

        public AccountRepository(ApplicationDbContext context, IPasswordHasher<Accounts> passwordHasher)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        /// <summary> Добавяне на нов акаунт </summary>
        public async Task<Accounts> RegisterAsync(JsonElement registerRequest)
        {
            RegisterDTO? account = null;
            ClientsRegisterDTO? client = null;
            RestaurantRegisterDTO? restaurant = null;
            short accountType = registerRequest.GetProperty("role").GetInt16();
                       
            if (accountType == (short)UserRoles.Restaurant)
            {
                restaurant = JsonSerializer.Deserialize<RestaurantRegisterDTO>(registerRequest.GetRawText());
                account = restaurant.Account;
            }
            else if (accountType == (short)UserRoles.Client)
            {
                client = JsonSerializer.Deserialize<ClientsRegisterDTO>(registerRequest.GetRawText());
                account = client.Account;
            }
            else
                throw new ArgumentException("Invalid DTO type.");

            AccountRoleType? roleFromDb = await _context.AccountRoleTypes.FirstOrDefaultAsync(r => r.Id == accountType);
            if (roleFromDb == null)
                throw new ArgumentNullException(nameof(roleFromDb));

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                ErrorMsg? result = null;
                try
                {
                    // Валидация на входни данни при регистрация
                    ValidateNewUser validateNewUser = new ValidateNewUser(_context);
                    result = await validateNewUser.Validate(account).ConfigureAwait(false);
                    if (result != null)
                        throw result;

                    // Добавяне на нов акаунт
                    Accounts newAccount = new Accounts
                    {
                        Status = GlobalMethods.SetBit(new byte[] { 0x00 }, AccountStatusBits.STS_ACTIVE, true),
                        Username = account.Username,
                        Password = _passwordHasher.HashPassword(null, account.Password),
                        Email = account.Email,
                        Phone = account.PhoneNumber,
                        Role = roleFromDb,
                        AccessFailCount = 5,
                        CreatedAt = DateTime.Now,
                        LastChangeAt = DateTime.Now,
                        BlockedAt = SYSTEM_DEFINES.NON_DATE
                    };

                    _context.Accounts.Add(newAccount);
                    await _context.SaveChangesAsync();

                    // Добавяне на нов запис за клиент
                    if(client != null)
                    {

                        var newClient = new Clients
                        {
                            Account = newAccount,
                            FirstName = client.FirstName,
                            LastName = client.LastName,
                            Points = 0
                        };

                        _context.Clients.Add(newClient);
                        await _context.SaveChangesAsync();
                    }

                    // Добавяне на нов запис за ресторант 
                    else if (restaurant != null)
                    {

                        // Валидация на входни данни при регистрация
                        ValidateNewRestaurant validateNewRestaurant = new ValidateNewRestaurant(_context);
                        result = await validateNewRestaurant.Validate(restaurant).ConfigureAwait(false);

                        if (result != null)
                            throw result;

                        var newRestaurant = new Restaurants
                        {
                            Account = newAccount,
                            CompanyName = restaurant.CompanyName,
                            Description = restaurant.Description,
                            Address = restaurant.Address,
                            Bulstat = restaurant.Bulstat,
                            Atmosphere = restaurant.Atmosphere,
                        };

                        _context.Restaurants.Add(newRestaurant);
                        await _context.SaveChangesAsync();

                    }

                    await transaction.CommitAsync();
                    return newAccount;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        /// <summary> Влизане в акаунт </summary>
        public async Task<Accounts?> LoginAsync(LoginDTO dto)
        {
            Accounts? account = _context.Accounts
                    .Include(a => a.Role)
                    .FirstOrDefault(a => a.Username == dto.Username);

            //Проверка за открит акаунт по потребителско име
            if (account == null)
                throw new ErrorMsg(false, ERROR_MSG.MSG_ACCOUNTS_LOGIN_WRONG_USERNAME);

            //Проверка дали акаунта е блокиран
            if ( GlobalMethods.GetBit(account.Status, AccountStatusBits.STS_BLOCKED) )
                throw new ErrorMsg(false, ERROR_MSG.MSG_ACCOUNTS_LOGIN_USER_BLOCKED);

            //Проверка дали подадената парола е коректна
            if ( _passwordHasher.VerifyHashedPassword(account, account.Password, dto.Password) == PasswordVerificationResult.Failed )
            {
                //Ако не е увеличаваме брояча и блокираме потребителя
                account.CurrentAccessFailCount++;
                if (account.AccessFailCount == account.CurrentAccessFailCount)
                {
                    GlobalMethods.SetBit(account.Status, AccountStatusBits.STS_ACTIVE, false);
                    GlobalMethods.SetBit(account.Status, AccountStatusBits.STS_BLOCKED, true);
                    account.LastChangeAt = DateTime.UtcNow;
                    account.BlockedAt = DateTime.UtcNow;
                }

                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();

                throw new ErrorMsg(false, ERROR_MSG.MSG_ACCOUNTS_LOGIN_WRONG_PASSWORD);
            }

            return account;
        }
    }
}