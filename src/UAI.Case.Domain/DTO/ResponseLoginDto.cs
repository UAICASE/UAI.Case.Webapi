
using UAI.Case.Domain.Common;

namespace UAI.Case.Dto
{
    public class ResponseLoginDto
    {
        public string Token { get; set; }
        public Usuario Usuario { get; set; }
    }
}
