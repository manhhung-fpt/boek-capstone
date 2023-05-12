using System.Net;
using Boek.Core.Constants;

namespace Boek.Api;
public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILoggerFactory loggerFactory;
    private readonly IConfiguration _configuration;

    public async Task SendLogToTelegramChannel(string message)
    {
        var apiToken = _configuration.GetSection(MessageConstants.TELEGRAM_CONFIG_API_TOKEN).Value;
        var channelId = _configuration.GetSection(MessageConstants.TELEGRAM_CONFIG_CHANNEL_ID).Value;
        var telegramBaseUrl = MessageConstants.TELEGRAM_CONFIG_BASE_URL;
        var telegramRequestUrl = String.Format(telegramBaseUrl, apiToken, channelId, message);
        var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, telegramRequestUrl);
        await httpClient.SendAsync(request);
    }
    public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        this.next = next;
        this.loggerFactory = loggerFactory;
        _configuration = configuration;
    }
    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();

        string ip = context.Request.Headers[MessageConstants.MIDDLE_WARE_CONFIG_IP].FirstOrDefault();
        string userAgent = context.Request.Headers[MessageConstants.MIDDLE_WARE_CONFIG_USER_AGENT].FirstOrDefault();
        var logger = loggerFactory.CreateLogger(MessageConstants.MIDDLE_WARE_CONFIG_LOGGER);

        using (var reader = new StreamReader(
        context.Request.Body,
        encoding: System.Text.Encoding.UTF8,
        detectEncodingFromByteOrderMarks: false,
        bufferSize: 4096,
        leaveOpen: true))
        {
            var requestBody =
            context.Request.ContentType == MessageConstants.MIDDLE_WARE_CONFIG_JSON_CONTENT_TYPE ?
            await reader.ReadToEndAsync() : MessageConstants.MIDDLE_WARE_CONFIG_OTHER_CONTENT_TYPE;

            // Do some processing with body…
        var requestLog = @$"📥 [REQUEST]
👤 Client IP: {ip}
🕵️ User-Agent: {userAgent}
🛣️ Path: {context.Request.Path}
🤖 Method: {context.Request.Method}
🔍 Query: {context.Request.QueryString}
💾 Request Body: {requestBody}
📝 Content-Type: {context.Request.ContentType}
📏 Content-Length: {context.Request.ContentLength}";

            logger.LogInformation(requestLog);
            try
            {
                await SendLogToTelegramChannel(requestLog);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            // Reset the request body stream position so the next middleware can read it
            context.Request.Body.Position = 0;
        }

        await next(context);
        var responseString =
        @$"📤 [RESPONSE]
👤 Client IP: {ip}
🛣️ Path: {context.Request.Path}
🤖 Method: {context.Request.Method}
🔍 Query: {context.Request.QueryString}
🔢 Status Code: {context.Response.StatusCode}
📝 Content-Type: {context.Response.ContentType}
📏 Content-Length: {context.Response.ContentLength}";
        int statusCode = context.Response.StatusCode;
        if (statusCode < 300)
        {
            logger.LogInformation(responseString);
        }
        else if (statusCode >= 400 && statusCode <= 500)
        {
            logger.LogWarning(responseString);
        }
        else
        {
            logger.LogError(responseString);
        }
        try
        {
            await SendLogToTelegramChannel(responseString);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
