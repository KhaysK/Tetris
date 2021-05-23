public class Block : TetrisBlocks
{
    void Update()
    {
        Move();
        Rotation(-90);
    }
}
