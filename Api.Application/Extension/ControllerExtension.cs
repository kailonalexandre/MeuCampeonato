using Microsoft.Extensions.Primitives;

namespace Api.application.Extension
{
    public static class ControllerExtension
    {
        /// <summary>
        /// Recebe o token de autenticação de usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetToken(this HttpRequest request)
        {
            var re = request;
            var headers = re.Headers;

            if (headers.TryGetValue("Authorization", out StringValues token))
            {
                if (string.IsNullOrEmpty(token))
                {
                    return string.Empty;
                }
            }
            return token;
        }
    }
}
