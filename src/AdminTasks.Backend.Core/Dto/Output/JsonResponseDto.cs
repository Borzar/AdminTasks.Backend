namespace Dto.Output;

public class JsonResponseDto
{
    public string StatusDto { get; set; }
    public string DescriptionDto { get; set; }
    public List<TaskOutputDto> ResultDto { get; set; }

}