using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UAI.Case.Webapi.hubs
{
    internal class UaiCaseElementHolder
    {
        private readonly IList<HoldDTO> _holders = new List<HoldDTO>();


        public IList GetAllHolds(Guid roomId)
        {

            if (_holders != null)
            {
                var h = _holders.Where(_ => _.RoomId.Equals(roomId)).ToList();
                return h;
            }
            else
                return null;
        }

        public void ReleaseAll(Guid userId)
        {
            var borrar = _holders.Where(_ =>  _.Usuario.Id.Equals(userId));

            foreach (var item in borrar.ToList())
            {
                _holders.Remove(item);
            }
        }

        public void Release(HoldDTO dto)
        {
            //suelto la seleccion y (por las dudas) quito todas las selecciones (error de socket?)
            ReleaseAll(dto.Usuario.Id);

        }



        //private void RemoveAllHolds(HoldDTO dto)
        //{
        //    //var borrar = _holders.Where(_ => _.RoomId.Equals(dto.RoomId) && _.Usuario.Id.Equals(dto.Usuario.Id));
        //    var borrar = _holders.Where(_ =>  _.Usuario.Id.Equals(dto.Usuario.Id)); //saque e room, deberia andar

        //    foreach (var item in borrar.ToList())
        //    {
        //        _holders.Remove(item);
        //    }
        //}

        public void Hold(HoldDTO dto)
        {

                      
            lock (_holders)
            {
                //me fijo que este usuario no tenga ninguna seleccion previa en ese room, y si tiene la elimino
                ReleaseAll(dto.Usuario.Id);
                //agrego la nueva seleccion
                _holders.Add(dto);
            }
        }


    }
}