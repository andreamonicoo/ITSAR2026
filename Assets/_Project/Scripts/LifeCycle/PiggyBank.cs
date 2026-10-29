using System.Collections;                     // Importa il namespace che contiene IEnumerator, necessario per le coroutine
using UnityEngine;                            // Importa le classi base di Unity (MonoBehaviour, Debug, WaitForSeconds, ecc.)

public class PiggyBank : MonoBehaviour        // Dichiara la classe PiggyBank (salvadanaio), che eredita da MonoBehaviour e quindi può stare su un GameObject
{                                             // Apre il corpo della classe
    [SerializeField] int maxCapacity = 10;    // Capacità massima del salvadanaio (10); SerializeField la rende modificabile dall'Inspector pur essendo privata
    [SerializeField] int currentValue;        // Valore attuale contenuto nel salvadanaio, visibile nell'Inspector

    [SerializeField] BankAccount bankAccount; // Riferimento a un componente BankAccount (il conto in banca), da assegnare nell'Inspector

    private void Awake()                      // Metodo di Unity chiamato per primo, quando l'oggetto viene caricato (prima di Start)
    {                                         // Apre il corpo di Awake
        currentValue = 0;                     // Azzera il valore del salvadanaio all'avvio
    }                                         // Chiude Awake

    private void Start()                      // Metodo di Unity chiamato una volta, prima del primo frame, dopo Awake
    {                                         // Apre il corpo di Start
        StartCoroutine(AddMoneyToPiggyBank());// Avvia la coroutine che aggiunge soldi al salvadanaio nel tempo
    }                                         // Chiude Start
    IEnumerator AddMoneyToPiggyBank()         // Dichiara la coroutine (restituisce IEnumerator, così può "mettersi in pausa")
    {                                         // Apre il corpo della coroutine
        yield return new WaitForSeconds(1);   // Mette in pausa la coroutine per 1 secondo senza bloccare il gioco
        currentValue++;                       // Passato il secondo, aggiunge 1 al valore del salvadanaio

        if (currentValue == maxCapacity)      // Controlla se il salvadanaio ha raggiunto la capacità massima
        {                                     // Apre il blocco dell'if
            AddToAccount();                   // Se è pieno, chiama il metodo che versa i soldi sul conto
        }                                     // Chiude il blocco dell'if

        StartCoroutine(AddMoneyToPiggyBank());// Riavvia la stessa coroutine, creando un ciclo infinito che aggiunge 1 ogni secondo
    }                                         // Chiude la coroutine

    void AddToAccount()                       // Metodo che trasferisce il contenuto del salvadanaio sul conto
    {                                         // Apre il corpo del metodo
        if(bankAccount)                       // Controlla che il riferimento al conto sia stato assegnato (non sia null)
        {                                     // Apre il blocco dell'if
            bankAccount.AddMoneyToBank(currentValue);                 // Chiama il metodo del conto per versare il valore attuale del salvadanaio
            Debug.Log($"MONEY IN THE BANK: {bankAccount.BankValue}"); // Stampa in Console il saldo aggiornato del conto (stringa interpolata con $)
            currentValue = 0;                 // Svuota il salvadanaio dopo il versamento
        }                                     // Chiude il blocco dell'if
    }                                         // Chiude AddToAccount
}                                             // Chiude la classe PiggyBank
