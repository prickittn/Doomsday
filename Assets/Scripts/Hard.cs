using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hard : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    public int mineCount = 32;

    private Board board;
    private Cell[,] state;
    private bool gameover;

    public Shaker Shaker;
    public float duration = 1f;

    AudioManager audioManager;

    public TMP_Text MineCountTxt;

    public float TimeLeft;
    public bool TimerOn = false;
    public TMP_Text TimerTxt;

    public GameOverScreenHard gameOverScreen;
    public GameWinScreenHard gameWinScreen;

    private void OnValidate()
    {
        mineCount = Mathf.Clamp(mineCount, 0, width * height);
    }
 
    private void Awake()
    {
        board = GetComponentInChildren<Board>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.menuClick);
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        state = new Cell[width, height];
        gameover = false;
        TimerOn = true;
        Time.timeScale = 1f;
        MineCountTxt.SetText(string.Format("Mines Left: {0}", mineCount));


        GenerateCells();
        GenerateMines();
        GenerateNumbers();

        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);
        board.Draw(state);
    }

    private void GenerateCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.Empty;
                state[x, y] = cell;
            }
        }
    }

    private void GenerateMines()
    {
        for(int i = 0; i < mineCount; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            while (state[x, y].type == Cell.Type.Mine)
            {
                x++;

                if (x >= width)
                {
                    x = 0;
                    y++;

                    if (y >= height) {
                        y = 0;
                    }
                }
            }

            state[x, y].type = Cell.Type.Mine;
        }
    }

    private void GenerateNumbers()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine) {
                    continue;
                }

                cell.number = CountMines(x, y);

                if (cell.number > 0) {
                    cell.type = Cell.Type.Number;
                }

                state[x, y] = cell;
            }
        }
    }

    private int CountMines(int cellX, int cellY)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for(int adjacentY = -1;adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0) {
                    continue;
                }

                int x = cellX + adjacentX;
                int y = cellY + adjacentY;

                if (GetCell(x, y).type == Cell.Type.Mine) {
                    count++;
                }
            }
        }

        return count;
    }

    private void Update()
    { 
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Cell cell = GetCell(1, 1);
                Debug.Log("Time is up!");
                TimeLeft = 0;
                TimerOn = false;
                Explode(cell);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            NewGame();
        }
        else if (!gameover && Time.timeScale == 1f)
        {
            if (Input.GetMouseButtonDown(1)) {
                Flag();
            } else if (Input.GetMouseButtonDown(0)) {
                Reveal();
            }
        }
    }

    private void Flag()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed) {
            return;
        }

        if (cell.flagged == true){
            this.mineCount ++;
        }
        else{
            this.mineCount --;
        }

        cell.flagged = !cell.flagged; 
        state[cellPosition.x, cellPosition.y] = cell;
        audioManager.PlaySFX(audioManager.flag);
        MineCountTxt.SetText(string.Format("Mines Left: {0}", mineCount));
        board.Draw(state);
    }

    private void Reveal ()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged) {
            return;
        }

        switch (cell.type)
        {
            case Cell.Type.Mine:
                Explode(cell);
                break;

            case Cell.Type.Empty:
                audioManager.PlaySFX(audioManager.flood);
                Flood(cell);
                CheckWinCondition();
                break;

            default:
                cell.revealed = true;
                audioManager.PlaySFX(audioManager.cellReveal);
                state[cellPosition.x, cellPosition.y] = cell;
                CheckWinCondition();
                break;
        }

        board.Draw(state);
    }

    private void Flood(Cell cell)
    {
        if (cell.revealed) return;
        if (cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid) return;

        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        if (cell.type == Cell.Type.Empty)
        {
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
            Flood(GetCell(cell.position.x, cell.position.y + 1));
            Flood(GetCell(cell.position.x - 1, cell.position.y - 1));
            Flood(GetCell(cell.position.x - 1, cell.position.y + 1));
            Flood(GetCell(cell.position.x + 1, cell.position.y - 1));
            Flood(GetCell(cell.position.x + 1, cell.position.y + 1));
        }
    }

    private void Explode(Cell cell)
    {
        Debug.Log("you lost, dummy.");

        Shaker.Shake(duration);

        audioManager.StopMusic();
        audioManager.PlaySFX(audioManager.bombExplode);
        audioManager.PlaySFX(audioManager.failure);

        Time.timeScale = 0f;

        gameover = true;

        cell.revealed = true;
        cell.exploded = true;
        state[cell.position.x, cell.position.y] = cell;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                    state[x, y] = cell;
                }
            }
        }
        gameOverScreen.Setup();
    }

    private void CheckWinCondition()
    { 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type != Cell.Type.Mine && !cell.revealed) {
                    return;
                }
            }
        }

        Debug.Log("Yippee!");
        gameover = true;
        audioManager.StopMusic();
        audioManager.PlaySFX(audioManager.success);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.flagged = true;
                    state[x, y] = cell;
                }
            }
        }
        gameWinScreen.Setup();
    }

    private Cell GetCell(int x, int y)
    {
        if (IsValid(x, y)) {
            return state[x, y];
        } else {
            return new Cell();
        }
    }

    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        if (seconds < 10)
        {
            TimerTxt.SetText(string.Format("{0}:0{1}", minutes, seconds));
        }
        else
        {
            TimerTxt.SetText(string.Format("{0}:{1}", minutes, seconds));
        }
    }

}