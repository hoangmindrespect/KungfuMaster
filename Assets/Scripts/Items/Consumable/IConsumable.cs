using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IConsumable
{
    void Consume();
    void Consume(List<BaseStat> stats);
}
