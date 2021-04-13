using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IControlable
{
   
    void youve_Been_tapped();
    void MoveTo(Vector3 distance);

    void deactiveSelectedObject();

    void reSize();

    void rotate();
}
