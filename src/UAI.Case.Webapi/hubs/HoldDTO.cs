using System;
using UAI.Case.Domain.Common;

namespace UAI.Case.Webapi.hubs
{
    public class HoldDTO
    {
        public Usuario Usuario { get; set; }
        public Guid RoomId { get; set; }
        public Guid ElementId { get; set; }
    }
}