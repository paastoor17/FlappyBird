using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instancia única del GameManager
    public TMP_Text scoreText;           // Texto que muestra la puntuación
    public TMP_Text bestScoreText;       // Texto que muestra el mejor puntaje
    public GameObject pipeSpawnerPrefab; // Referencia al prefab del spawner de tubos
    public GameObject birdPrefab;        // Referencia al prefab del pájaro
    private int score = 0;               // Puntuación actual
    private int bestScore = 0;           // Mejor puntuación guardada

    void Awake()
    {
        // Implementa el patrón de diseño Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // Mantiene el GameManager entre escenas
    }

    void Start()
    {
        LoadBestScore(); // Carga el mejor puntaje al inicio
        UpdateScore(0);  // Actualiza la puntuación inicial
    }

    // Método para actualizar la puntuación
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;

        // Compara y actualiza el mejor puntaje si es necesario
        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = "Best Score: " + bestScore;
            SaveBestScore(); // Guarda el mejor puntaje
        }
    }

    // Método para guardar el mejor puntaje
    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }

    // Método para cargar el mejor puntaje
    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore;
    }

    // Método para reiniciar el nivel
    public void RestartGame()
    {
        score = 0;
        UpdateScore(0); // Reinicia la puntuación

        // Resetea los componentes del juego
        ResetBird();
        ResetPipeSpawner();

        // Carga la escena actual para reiniciar el nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método para resetear el pájaro
    private void ResetBird()
    {
        BirdController bird = FindObjectOfType<BirdController>();
        if (bird != null)
        {
            bird.ResetBird(); // Resetea el estado del pájaro
        }
    }

    // Método para resetear el spawner de tubos
    private void ResetPipeSpawner()
    {
        PipeSpawner pipeSpawner = FindObjectOfType<PipeSpawner>();
        if (pipeSpawner != null)
        {
            pipeSpawner.ResetSpawner(); // Resetea el spawner de tubos
        }
    }

    // Método para mostrar la pantalla de Game Over
    public void ShowGameOver()
    {
        Time.timeScale = 0f; // Pausa el tiempo del juego
        GameObject.FindObjectOfType<GameOverManager>().ShowGameOver(); // Muestra el Game Over
    }
}
