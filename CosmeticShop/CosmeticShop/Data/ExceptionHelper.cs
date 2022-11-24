namespace CosmeticShop.WebApp.Data
{
    public static class ExceptionHelper
    {
        public static void ThrowIfObjectWasNull(object dataObject, string objectName, string methodName = "")
        {
            if (dataObject is null)
            {
                throw new ArgumentNullException(objectName, $"Object was null in {methodName}");
            }
        }
    }
}
