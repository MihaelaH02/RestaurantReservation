
namespace RestaurantReservation.Common
{
    public static class GlobalMethods
    {

        public static string GetEnumDescription(Enum enumElemen)//?
        {
            return enumElemen.ToString();
        }

        public static byte[] SetBit(byte[] status, int bit, bool flag)
        {
            int byteIndex = bit / 8;
            int bitPosition = bit % 8;

            byte targetByte = status[byteIndex];
            byte mask = (byte)(1 << bitPosition);
            byte updatedByte;

            if (flag)
                updatedByte = (byte)(targetByte | mask);
            else
                updatedByte = (byte)(targetByte & (~mask));

            byte[] updatedBinaryData = status.ToArray();
            updatedBinaryData[byteIndex] = updatedByte;

            return updatedBinaryData;
        }

        public static bool GetBit(byte[] status, int bit)
        {
            int byteIndex = bit / 8;
            int bitPosition = bit % 8;

            byte targetByte = status[byteIndex];
            byte bitMask = (byte)(1 << bitPosition);

            return (targetByte & bitMask) == bitMask;
        }
    }
}
