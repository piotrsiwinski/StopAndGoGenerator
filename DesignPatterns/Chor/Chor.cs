using System.Collections.Generic;
using DesignPatterns.Chor.Abstract;

namespace DesignPatterns.Chor
{
    public class Chor
    {
        protected Dictionary<string, IHandler> Handlers { get; set; }

        public virtual void Handle(HandlerRefType entity)
        {
            foreach (var handler in Handlers)
            {
                handler.Value.Handle(entity);
            }
        }
    }
}