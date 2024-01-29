using ControlRoomApi.Authentication.Dto;

namespace ControlRoomApi.Authentication
{
    internal class BadRequestException : Exception
    {
        public ErrorDto ErrorDto { get; set; }

        public BadRequestException(ErrorDto errorDto) : base($"[{errorDto.Code}] {errorDto.Message} ({errorDto.Details})")
        {
            ErrorDto = errorDto;
        }
    }

    internal class UnauthorizedAccessException : Exception
    {
        public ErrorDto ErrorDto { get; set; }
        public UnauthorizedAccessException(ErrorDto errorDto) : base($"[{errorDto.Code}] {errorDto.Message} ({errorDto.Details})") 
        { 
            ErrorDto = errorDto;
        }
    }

    internal class InternalServerErrorException : Exception
    {
        public ErrorDto ErrorDto { get; set; }
        public InternalServerErrorException(ErrorDto errorDto) : base($"[{errorDto.Code}] {errorDto.Message} ({errorDto.Details})")
        {
            ErrorDto = errorDto;
        }
    }
}
