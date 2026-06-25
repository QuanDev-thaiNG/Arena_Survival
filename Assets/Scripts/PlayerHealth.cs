using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    private void Awake() => _currentHealth = _maxHealth;

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        Debug.Log($"Player HP: {_currentHealth}/{_maxHealth}");

        if (_currentHealth <= 0f)
            Debug.Log("Player died!");
    }
}