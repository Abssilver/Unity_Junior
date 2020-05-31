using UnityEngine;
using Gameplay.Weapons.Projectiles;

public class Rocket : Projectile
{
    //можно реализовать отдельную логику для движения ракеты 
    //например, с помощью рейкаста захватывать цель перед носом корабля и перемещать по ракету к цели
    //если при запуске hit null, то ракета летит просто вперед
    //пока она ничем не отличается от лазера. При этом у префаба изменены значения урона и скорости
    //которые будут соответсвовать ракете.
    protected override void Move(float speed)
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }
}
