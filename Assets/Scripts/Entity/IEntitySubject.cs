using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntitySubject
{
    public void Attach(EntityObserverType type, IEntityObserver ob);
    public void Dettach(EntityObserverType type, IEntityObserver ob);
    public void NotifyAllObserver(EntityObserverType type);
}
