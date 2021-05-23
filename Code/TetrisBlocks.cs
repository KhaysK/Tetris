using UnityEngine;

public class TetrisBlocks : MonoBehaviour
{
    float isMovedTime;
    float fallTime = 0.8f;

    static int height = 16;
    static int width = 10;
    static Transform[,] grid = new Transform[width, height];

    public void Move()
    {
        // Реализует движение персонажа 

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (OverBoard()) transform.position -= new Vector3(-1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if(OverBoard()) transform.position -= new Vector3(1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))        
            fallTime = fallTime / 10;


        if (Mathf.Abs(Time.time - isMovedTime) > fallTime)
        {
            transform.position += new Vector3(0, -1, 0);
            if (OverBoard())
            {
                transform.position -= new Vector3(0, -1, 0);
                AdToGrid();
                CheckForLines();
                CheckForEnd();
                this.enabled = false;
                FindObjectOfType<Spawner>().SpawnBlock();

            }
            isMovedTime = Time.time;
        }
            
    }

    public void Rotation(int angle)
    {
        // Вращает объект при нажатии на кнопку
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, angle);
            if (OverBoard()) transform.Rotate(0, 0, -angle);
        }
           
    }

    void AdToGrid()
    {
        // Добавление кубика к сетке 
        foreach (Transform children in transform)
        {
            int X = Mathf.RoundToInt(children.transform.position.x);
            int Y = Mathf.RoundToInt(children.transform.position.y);

            grid[X, Y] = children;
        }
    }

    bool OverBoard()
    {
        // Проверка зашел ли обьект за границы сетки
        foreach(Transform children in transform)
        {
            int X = Mathf.RoundToInt(children.transform.position.x);
            int Y = Mathf.RoundToInt(children.transform.position.y);

            if (X < 0 || X >= width || Y < 0 || Y >= height)
                return true;
            
            if (grid[X, Y] != null)
                return true;
                
        }
        return false;
    }

    void CheckForLines()
    {
        // Проверка на заполнение линии и удаление ее с получением очков 
        for(int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                FindObjectOfType<UIManager>().score += 100;
            }
        }
    }

    bool HasLine(int i)
    {
        // Проверяет заполнена ли строка кубиками(объектами), если видит хоть одну пустую ячейку выдает false
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

    void DeleteLine(int i)
    {
        // Удаляет все кубики(объекты) в строке 
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        // Спускает кубик ниже если ячейка снизу пустая
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if(grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }

            }
        }
    }

    void CheckForEnd()
    {   
        for (int j = 0; j < width; j++)
        {
            if (grid[j, 15] != null)
                FindObjectOfType<UIManager>().EndGame();
        }
    }
}
