using System.Collections;
using System.Collections.Generic;

public interface IHealable
{
    void ReceiveHealing<T>(T healAmount);
}
