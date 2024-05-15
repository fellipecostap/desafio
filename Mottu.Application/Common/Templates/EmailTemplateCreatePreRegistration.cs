using System;

namespace Desafio.Application.Common.Templates
{
    public class EmailTemplateCreatePreRegistration
    {
        public string EmailTemplateBodyHtml { get; set; } = @"<html xmlns=""http://www.w3.org/1999/xhtml"">
            <head>
                <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
                <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
                <link href=""https://fonts.googleapis.com/css2?family=Montserrat&display=swap"" rel=""stylesheet"">
                <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
                <title>Desafio - Confirme seu registro</title>
            </head>
            <body style=""background-color: #eaeced; font-family: 'Montserrat', sans-serif; display: flex; align-items: center; justify-content: center; padding: 28px;"">
                <div style=""background-color: #fff; border-radius: 8px; padding: 20px; text-align: center; margin: auto; max-width: 400px;"">
                    <h1 style=""font-size: 24px; margin-top: 10px;"">Olá, {0}</h1>
                    <p style=""color: #6E6D72; font-size: 16px;"">Obrigado por escolher a Desafio!</p>
                    <p style=""color: #6E6D72; font-size: 16px;"">Para confirmar seu registro, utilize o código de confirmação abaixo:</p>
                    <h2 style=""font-size: 28px; color: #333; margin-top: 10px;"">{1}</h2>
                    <p style=""color: #6E6D72; font-size: 16px;"">Insira este código no aplicativo Desafio para finalizar seu registro.</p>
                    <div style=""text-align: center;"">
                        <img src=""https://tratobuckets3.s3.us-east-2.amazonaws.com/Desafio.png"" width=""60"" height=""60"">
                    </div>
                </div>
            </body>
        </html>";
    }
}
