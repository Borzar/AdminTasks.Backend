using Dto.Output;

namespace Models.Output;

public class JsonResponse
{
    public string Status { get; set; }
    public string Description { get; set; }
    public List<TaskOutput> Result { get; set; }

}