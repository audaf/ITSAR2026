using System;
using UnityEngine;
using UnityEngine;

/// <summary>
/// Area con trigger che applica un effetto principale (danno o cura nel tempo)
/// ed effetti opzionali (scivolamento, rallentamento/accelerazione) al Player.
/// </summary>
[RequireComponent(typeof(Collider))]
public class AreaEffect : MonoBehaviour
{
    [Flags]
    public enum AreaType
    {
        Damage,
        Heal
    }

    [Header("Effetto principale")]
    [SerializeField] private AreaType areaType = AreaType.Damage;
    [SerializeField, Min(0f)] private float amount = 10f;
    [SerializeField, Min(0.05f), Tooltip("Secondi tra un tick e l'altro")] private float tickInterval = 1f;

    [Header("Filtro")]
    [SerializeField] private LayerMask targetLayer;   // imposta Player

    [Header("Scivolamento (opzionale)")]
    [SerializeField] private bool enableSlide = false;
    [SerializeField, Min(0.1f), Tooltip("Più basso = più ghiaccio")] private float slideAcceleration = 2f;

    [Header("Rallentamento / Accelerazione (opzionale)")]
    [SerializeField] private bool enableSpeedModifier = false;
    [SerializeField, Tooltip("<1 rallenta, >1 accelera")] private float speedMultiplier = 0.5f;
    [SerializeField, Min(0f), Tooltip("Durata dopo l'attivazione, anche fuori dall'area")] private float speedModifierDuration = 5f;

    private Health health;
    private PlayerController player;
    private float nextTickTime;

    private void Reset()
    {
        // Il collider deve essere un trigger.
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsTarget(other)) return;

        other.TryGetComponent(out health);
        other.TryGetComponent(out player);

        nextTickTime = Time.time;   // primo tick immediato

        if (enableSpeedModifier && player != null)
        {
            player.ApplySpeedModifier(speedMultiplier, speedModifierDuration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsTarget(other)) return;

        // Se il player è stato disattivato/riattivato i riferimenti potrebbero mancare.
        if (health == null && player == null)
        {
            other.TryGetComponent(out health);
            other.TryGetComponent(out player);
        }

        // Effetto periodico
        if (health != null && !health.IsDead && Time.time >= nextTickTime)
        {
            nextTickTime = Time.time + tickInterval;

            if (areaType == AreaType.Damage) health.TakeDamege(amount);
            else health.Heal(amount);
        }

        if (player != null)
        {
            if (enableSlide)
            {
                player.ApplySlide(slideAcceleration);
            }

            // Rinnova il timer finché il player sta nell'area.
            if (enableSpeedModifier)
            {
                player.ApplySpeedModifier(speedMultiplier, speedModifierDuration);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsTarget(other)) return;

        // Il modificatore di velocità NON viene rimosso: scade da solo dopo la durata.
        health = null;
        player = null;
    }

    private bool IsTarget(Collider other)
    {
        return (targetLayer.value & (1 << other.gameObject.layer)) != 0;
    }
}