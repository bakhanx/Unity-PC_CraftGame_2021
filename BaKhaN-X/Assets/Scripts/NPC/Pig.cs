using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : WeakAnimal
{
    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();
    }

    protected void RandomAction()
    {
        RandomSound();

        int _random = Random.Range(0, 4); // wait, eat, peek, walk // range :  0~3 

        if (_random == 0)
            Wait();

        else if (_random == 1)
            Eat();

        else if (_random == 2)
            Peek();

        else if (_random == 3)
            TryWalk();

    }
    private void Wait()
    {
        currentTime = waitTime;
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
    }
}
