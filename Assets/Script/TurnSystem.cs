using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public enum TurnStates
    {
        TURNSTATE_IDLE,
        TURNSTATE_BEGINTURN,
        TURNSTATE_MAINFACE,
        TURNSTATE_ATTACK_DECLARATION,
        TURNSTATE_BLOCK_DECLARATION,
        TURNSTATE_COMBAT_RESOLVE,
        TURNSTATE_SECOND_MAINFACE,
        TURNSTATE_ENDTURN
    }

    public TurnStates turnState = TurnStates.TURNSTATE_IDLE;

    public GameObject newGame;
    public Text endGameText;

    public GameObject playerHand;
    public GameObject enemyHand;

    public Text playerHealthText;
    public Text enemyHealthText;

    public Text playerManaText;
    public Text enemyManaText;

    public GameObject endMainfaceButton;
    public GameObject endAttackDeclarationButton;
    public GameObject endBlockDeclarationButton;
    public GameObject endSecondMainfaceButton;

    public readonly int MAX_MANA = 12;
    public readonly int MAX_HEALTH = 30;
    public int currentPlayerMana;
    public int playerMana;
    public int currentEnemyMana;
    public int enemyMana;

    public int playerHealth;
    public int enemyHealth;

    public int currentPlayerTurn;
    public int currentEnemyTurn;

    public bool playerTurn;

    public bool TurnBeginRoutineIsDone = false;
    public bool CombatRoutineIsDone = false;
    public bool PlayCardIsDone = false;

     void Awake()
    {
        turnState = TurnStates.TURNSTATE_IDLE;
        //SetUp starting game values
        currentPlayerTurn = 1;
        currentEnemyTurn = 1;
        playerHealth = MAX_HEALTH;
        enemyHealth = MAX_HEALTH;
        currentPlayerMana = 0;
        currentEnemyMana = 0;

        // TODO: Setup starting player
        playerTurn = true;

        //SetUp UI
        playerHealthText.text = "" + playerHealth;
        enemyHealthText.text = "" + enemyHealth;

        playerManaText.text = "" + playerMana + "/" + currentPlayerMana;
        enemyManaText.text = "" + enemyMana + "/" + currentEnemyMana;
    }

    void Update()
    {
        switch (turnState)
        {
            case TurnStates.TURNSTATE_BEGINTURN:
                StartCoroutine(TurnBeginRoutine());
                break;
            case TurnStates.TURNSTATE_MAINFACE:
                OnMainface();
                break;
            case TurnStates.TURNSTATE_ATTACK_DECLARATION:
                OnAttackDeclaration();
                break;
            case TurnStates.TURNSTATE_BLOCK_DECLARATION:
                OnBlockDeclaration();
                break;
            case TurnStates.TURNSTATE_COMBAT_RESOLVE:
                StartCoroutine(CombatRoutine());
                break;
            case TurnStates.TURNSTATE_SECOND_MAINFACE:
                OnSecondMainFace();
                break;
            case TurnStates.TURNSTATE_ENDTURN:
                OnTurnEnd();
                break;
            case TurnStates.TURNSTATE_IDLE:
                OnIdle();
                break;
        }

        playerManaText.text = "" + playerMana + "/" + currentPlayerMana;
        enemyManaText.text = "" + enemyMana + "/" + currentEnemyMana;

        playerHealthText.text = "" + playerHealth;
        enemyHealthText.text = "" + enemyHealth;

        if (playerHealth <= 0)
        {
            this.endGameText.text = "Przegrana!";
            newGame.SetActive(true);
        }
        
        if(enemyHealth <= 0)
        {
            this.endGameText.text = "Wygrana!";
            newGame.SetActive(true);
        }
    }

    private void OnTurnBegin()
    {
        if (playerTurn)
        {
            //Player mana SetUp
            if (currentPlayerMana < MAX_MANA)
                currentPlayerMana++;
            else
                currentPlayerMana = MAX_MANA;

            playerMana = currentPlayerMana;
            playerManaText.text = "" + playerMana + "/" + currentPlayerMana;

            GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerCard");

            foreach (var o in objects)
            {
                o.GetComponent<Draggable>().isDraggable = false;
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    o.GetComponent<Card>().cardHealth = o.GetComponent<Card>().cardMaxHealth;
                    o.GetComponent<Card>().cardAttack = o.GetComponent<Card>().cardMaxAttack;
                }
            }

            GameObject.Find("PlayerArtifact").GetComponent<ArtifactBehavior>().enabled = true;
            GameObject.Find("PlayerArtifact").GetComponent<ArtifactBehavior>().enabled = false;

            // Player draw at start of turn
            if (currentPlayerTurn != 1)
                GameObject.Find("PlayerDeck").GetComponent<DrawCard>().Draw();
        }
        else
        {
            //Enemy mana SetUp
            if (currentEnemyMana < MAX_MANA)
                currentEnemyMana++;
            else
                currentEnemyMana = MAX_MANA;

            enemyMana = currentEnemyMana;
            enemyManaText.text = "" + enemyMana + "/" + currentEnemyMana;
            GameObject[] objects = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in objects)
            {
                o.GetComponent<Draggable>().isDraggable = false;
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    o.GetComponent<Card>().cardHealth = o.GetComponent<Card>().cardMaxHealth;
                    o.GetComponent<Card>().cardAttack = o.GetComponent<Card>().cardMaxAttack;
                }
            }

            GameObject.Find("EnemyArtifact").GetComponent<ArtifactBehavior>().enabled = true;
            GameObject.Find("EnemyArtifact").GetComponent<ArtifactBehavior>().enabled = false;

            //Enemy draw at start of turn
            if (currentEnemyTurn != 1)
                GameObject.Find("EnemyDeck").GetComponent<DrawCard>().Draw();
        }
    }

    private void OnMainface()
    {
        if (playerTurn)
        {
            //Set button ending mainface to Active
            endMainfaceButton.gameObject.SetActive(true);

            GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerCard");

            foreach (var o in objects)
            {
                o.GetComponent<Draggable>().isDraggable = true;

                if(o.GetComponent<Card>().cardCost <= playerMana)
                {
                    if (!o.GetComponent<CardController>().isOnGameField)
                        o.GetComponent<CardController>().canBePlayed = true;
                }
                else
                {
                    if (!o.GetComponent<CardController>().isOnGameField)
                        o.GetComponent<CardController>().canBePlayed = false;
                }
                    
                o.GetComponent<SelectCard>().enabled = false;
            }
        }
        else 
        {
            EnemyMainfaceLogic();
            turnState = TurnStates.TURNSTATE_ATTACK_DECLARATION;
        }
        
    }

    private void OnAttackDeclaration()
    {
        endMainfaceButton.gameObject.SetActive(false);

        if (playerTurn)
        {
            endAttackDeclarationButton.gameObject.SetActive(true);

            GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerCard");

            foreach (var o in objects)
            {
                o.GetComponent<SelectCard>().enabled = true;
                o.GetComponent<CardController>().canBePlayed = false;
            }
        }
        else
        {
            //StartCoroutine(AttackDeclarationRoutine());
            //if(AttackDeclarationIsDone)
                EnemyAttackLogic();
            turnState = TurnStates.TURNSTATE_BLOCK_DECLARATION;
        }
       
    }

    private void OnBlockDeclaration()
    {
        endAttackDeclarationButton.gameObject.SetActive(false);
        if (!playerTurn)
        {
            endBlockDeclarationButton.gameObject.SetActive(true);
            GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerCard");
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            int numberOfAttacking = 0;

            foreach(var o in enemyCards)
            {
                if (o.gameObject.GetComponent<CardController>().isOnGameField)
                {
                    if (o.gameObject.GetComponent<CardController>().isAttacking)
                        numberOfAttacking++;
                }
            }

            foreach (var o in objects)
            {
                if (o.gameObject.GetComponent<CardController>().isOnGameField)
                {
                    if(o.gameObject.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER && numberOfAttacking > 0)
                    {
                        o.gameObject.GetComponent<Card>().targetFrame.SetActive(true);
                        o.gameObject.GetComponent<CancelBlock>().enabled = true;
                    }
                }    
            }
        }
        else
        {
            //TODO: ENEMY BEHAVIOR IN BLOCK DECLARATION
            EnemyBlockLogic();
            turnState = TurnStates.TURNSTATE_COMBAT_RESOLVE;
        }
    }

    private void OnCombatResolve()
    {
        
        if (playerTurn)
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("PlayerCard");
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("EnemyCard");


            foreach (var o in playerObjects)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (!o.GetComponent<CardController>().isBlocked && o.GetComponent<CardController>().isAttacking)
                        enemyHealth -= o.GetComponent<Card>().cardAttack;
                }
            }

            foreach (var o in enemyObjects)
            {
                if (o.GetComponent<CardController>().target != null)
                {
                    if (o.GetComponent<CardController>().hasPercingAttack)
                    {
                        o.GetComponent<CardController>().target.GetComponent<Card>().cardHealth -= o.GetComponent<Card>().cardAttack;
                        o.GetComponent<Card>().cardHealth -= o.GetComponent<CardController>().target.GetComponent<Card>().cardAttack;
                        playerHealth -= (o.GetComponent<Card>().cardAttack + o.GetComponent<CardController>().target.GetComponent<Card>().cardHealth);
                    }
                    else
                    {
                        o.GetComponent<CardController>().target.GetComponent<Card>().cardHealth -= o.GetComponent<Card>().cardAttack;
                        o.GetComponent<Card>().cardHealth -= o.GetComponent<CardController>().target.GetComponent<Card>().cardAttack;
                    }

                    o.GetComponent<CardController>().target.GetComponent<CardController>().Cleric_Keyword();
                    o.GetComponent<CardController>().Cleric_Keyword();

                    if (o.GetComponent<CardController>().target.GetComponent<Card>().cardHealth <= 0)
                    {
                        o.GetComponent<CardController>().target.gameObject.GetComponent<Card>().RemoveFromTheField();
                        Destroy(o.GetComponent<CardController>().target.gameObject);
                    }

                    if (o.GetComponent<Card>().cardHealth <= 0)
                    {
                        o.gameObject.GetComponent<Card>().RemoveFromTheField();
                        Destroy(o.gameObject);
                    }

                    o.GetComponent<CardController>().target = null;
                }
            }


            foreach (var o in playerObjects)
            {
                if(o.GetComponent<CardController>().isAttacking)
                    o.GetComponent<SelectCard>().StopAttack();
            }
        }
        else 
        {
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("PlayerCard");
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("EnemyCard");

            foreach (var o in enemyObjects)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (!o.GetComponent<CardController>().isBlocked && o.GetComponent<CardController>().isAttacking)
                        playerHealth -= o.GetComponent<Card>().cardAttack;
                }
            }

            foreach(var o in playerObjects)
            {
                if (o.GetComponent<CardController>().target != null)
                {
                    if (o.GetComponent<CardController>().hasPercingAttack)
                    {
                        o.GetComponent<CardController>().target.GetComponent<Card>().cardHealth -= o.GetComponent<Card>().cardAttack;
                        o.GetComponent<Card>().cardHealth -= o.GetComponent<CardController>().target.GetComponent<Card>().cardAttack;
                        playerHealth -= (o.GetComponent<Card>().cardAttack + o.GetComponent<CardController>().target.GetComponent<Card>().cardHealth);
                    }
                    else
                    {
                        o.GetComponent<CardController>().target.GetComponent<Card>().cardHealth -= o.GetComponent<Card>().cardAttack;
                        o.GetComponent<Card>().cardHealth -= o.GetComponent<CardController>().target.GetComponent<Card>().cardAttack;
                    }

                    o.GetComponent<CardController>().target.GetComponent<CardController>().Cleric_Keyword();
                    o.GetComponent<CardController>().Cleric_Keyword();

                    if (o.GetComponent<CardController>().target.GetComponent<Card>().cardHealth <= 0)
                    {
                        o.GetComponent<CardController>().target.gameObject.GetComponent<Card>().RemoveFromTheField();
                        Destroy(o.GetComponent<CardController>().target.gameObject);
                    }

                    if (o.GetComponent<Card>().cardHealth <= 0)
                    {
                        o.gameObject.GetComponent<Card>().RemoveFromTheField();
                        Destroy(o.gameObject);
                    }

                    o.GetComponent<CardController>().target = null;
                }
            }

            foreach (var o in playerObjects)
            {
                if (o.gameObject.GetComponent<CardController>().isOnGameField)
                {
                    o.gameObject.GetComponent<Card>().targetFrame.SetActive(false);
                    o.gameObject.GetComponent<CancelBlock>().enabled = false;
                }
            }

            foreach(var o in enemyObjects)
            {
                if (o.GetComponent<CardController>().isAttacking)
                    o.GetComponent<SelectCard>().StopAttack();
            }

        }
    }
        

    private void OnSecondMainFace()
    {
        if (playerTurn)
        {
            endSecondMainfaceButton.gameObject.SetActive(true);
            GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerCard");

            foreach (var o in objects)
            {
                o.GetComponent<Draggable>().isDraggable = true;

                if (o.GetComponent<Card>().cardCost <= playerMana)
                {
                    if(!o.GetComponent<CardController>().isOnGameField)
                        o.GetComponent<CardController>().canBePlayed = true;
                }
                else
                {
                    if(!o.GetComponent<CardController>().isOnGameField)
                        o.GetComponent<CardController>().canBePlayed = false;
                }

                o.GetComponent<SelectCard>().enabled = false;
            }
        }
        else
        {
            EnemyMainfaceLogic();
            
            turnState = TurnStates.TURNSTATE_ENDTURN;
        }
    }

    private void OnTurnEnd()
    {
        endSecondMainfaceButton.gameObject.SetActive(false);
        if (playerTurn)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerCard");
            foreach(var o in objects)
            {
                if(o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                    o.GetComponent<CardController>().summoningSickness = false;
                o.GetComponent<CardController>().canBePlayed = false;
            }

            GameObject playerHand = GameObject.Find("PlayerHandZone");

            if (playerHand.transform.childCount > 7)
            {
                int r = Random.Range(0, playerHand.transform.childCount -1);
                playerHand.transform.GetChild(r).gameObject.GetComponent<Card>().RemoveFromTheField();
                Destroy(playerHand.transform.GetChild(r).gameObject);
            }

            playerTurn = false;
            currentEnemyTurn++;
            turnState = TurnStates.TURNSTATE_BEGINTURN;
        }
        else
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in objects)
            {
                if(o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                    o.GetComponent<CardController>().summoningSickness = false;
                o.GetComponent<CardController>().canBePlayed = false;
            }

            GameObject enemyHand = GameObject.Find("EnemyHandZone");

            if (enemyHand.transform.childCount > 7)
            {
                int r = Random.Range(0, enemyHand.transform.childCount - 1);
                enemyHand.transform.GetChild(r).gameObject.GetComponent<Card>().RemoveFromTheField();
                Destroy(enemyHand.transform.GetChild(r).gameObject);
            }

            playerTurn = true;
            currentPlayerTurn++;
            turnState = TurnStates.TURNSTATE_BEGINTURN;
        }
        
    }

    private void OnIdle()
    {
        
    }

    public void EndFirstMainface()
    {
        turnState = TurnStates.TURNSTATE_ATTACK_DECLARATION;
    }

    public void EndAttackDeclaration()
    {
        turnState = TurnStates.TURNSTATE_BLOCK_DECLARATION;
    }

    public void EndBlockDeclaration()
    {
        turnState = TurnStates.TURNSTATE_COMBAT_RESOLVE;
    }

    public void EndSecondMainface()
    {
        turnState = TurnStates.TURNSTATE_ENDTURN;
    }

    IEnumerator TurnBeginRoutine()
    {
        //Debug.Log("Początek Tury czekaj");
        TurnBeginRoutineIsDone = false;
        yield return new WaitForSeconds(2);
        if (!TurnBeginRoutineIsDone)
        {
            OnTurnBegin();
            turnState = TurnStates.TURNSTATE_MAINFACE;
            TurnBeginRoutineIsDone = true;
        }
    }

    IEnumerator CombatRoutine()
    {
        endBlockDeclarationButton.gameObject.SetActive(false);
        CombatRoutineIsDone = false;
        yield return new WaitForSeconds(3);
        if (!CombatRoutineIsDone)
        {
            OnCombatResolve();
            turnState = TurnStates.TURNSTATE_SECOND_MAINFACE;
            CombatRoutineIsDone = true;
        }
        
    }

    public void EnemyAttackLogic()
    {
        GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
        GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
        List<GameObject> playerCardsOnField = new List<GameObject>();
        List<GameObject> enemyCardsOnField = new List<GameObject>();

        foreach(var o in playerCards)
        {
            if (o.GetComponent<CardController>().isOnGameField)
                playerCardsOnField.Add(o);
        }

        foreach (var o in enemyCards)
        {
            if (o.GetComponent<CardController>().isOnGameField)
                enemyCardsOnField.Add(o);
        }

        if(playerCardsOnField.Count == 0)
        {
            foreach (var o in enemyCardsOnField)
            {
                if (!o.gameObject.GetComponent<CardController>().summoningSickness && !o.gameObject.GetComponent<CardController>().isDefender)
                {
                    selectAttack(o);
                }
            }
        }
        else
        {
            List<GameObject> atackingList = new List<GameObject>();
            if (enemyHealth > 10)
            {
                foreach (var o in enemyCardsOnField)
                {
                    foreach (var x in playerCardsOnField)
                    {
                        if(enemyCardsOnField.Count > playerCardsOnField.Count)
                        {
                            if (!o.gameObject.GetComponent<CardController>().isDefender)
                            {
                                if (o.GetComponent<Card>().cardHealth >= x.GetComponent<Card>().cardAttack && o.GetComponent<Card>().cardAttack >= x.GetComponent<Card>().cardHealth)
                                {
                                    atackingList.Add(o);
                                    break;
                                }

                                if (o.GetComponent<Card>().cardHealth >= x.GetComponent<Card>().cardAttack && o.GetComponent<Card>().cardAttack <= x.GetComponent<Card>().cardHealth)
                                {
                                    atackingList.Add(o);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (!o.gameObject.GetComponent<CardController>().isDefender)
                            {
                                if (o.GetComponent<Card>().cardHealth >= x.GetComponent<Card>().cardAttack && o.GetComponent<Card>().cardAttack >= x.GetComponent<Card>().cardHealth)
                                {
                                    atackingList.Add(o);
                                    break;
                                }
                                else if (o.GetComponent<Card>().cardHealth >= x.GetComponent<Card>().cardAttack && o.GetComponent<Card>().cardAttack <= x.GetComponent<Card>().cardHealth)
                                {
                                    atackingList.Add(o);
                                    break;
                                }
                                else if (o.GetComponent<Card>().cardHealth <= x.GetComponent<Card>().cardAttack)
                                {
                                    int r = Random.Range(0, 5);
                                    if (r > 3)
                                        atackingList.Add(o);
                                    else
                                        break;
                                }
                                else
                                {
                                    atackingList.Add(o);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var o in enemyCardsOnField)
                {
                    if (!o.gameObject.GetComponent<CardController>().summoningSickness && !o.gameObject.GetComponent<CardController>().isDefender)
                    {
                        foreach (var x in playerCardsOnField)
                        {
                            if (o.GetComponent<Card>().cardHealth <= x.GetComponent<Card>().cardAttack)
                            {
                                int r = Random.Range(0, 2);
                                if (r > 0)
                                    atackingList.Add(o);
                                else
                                    break;
                            }
                        }
                    }
                }
            }
            
            if(atackingList.Count > 0)
            {
                foreach(var o in atackingList)
                {
                    selectAttack(o);
                }
            }
      
        }
    }

    public void EnemyBlockLogic()
    {
        GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
        GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
        List<GameObject> playerCardsOnField = new List<GameObject>();
        List<GameObject> enemyCardsOnField = new List<GameObject>();

        foreach (var o in playerCards)
        {
            if (o.GetComponent<CardController>().isOnGameField)
                if(o.GetComponent<CardController>().isAttacking)
                    playerCardsOnField.Add(o);
        }

        foreach (var o in enemyCards)
        {
            if (o.GetComponent<CardController>().isOnGameField)
                enemyCardsOnField.Add(o);
        }

        if(playerCardsOnField.Count > 0)
        {
            if(enemyHealth < 10)
            {
                foreach (var o in playerCardsOnField)
                {
                    if (o.GetComponent<CardController>().isFlying)
                    {
                        foreach (var x in enemyCardsOnField)
                        {
                            if (x.GetComponent<CardController>().isFlying || x.GetComponent<CardController>().isArcher)
                            {
                                if (x.GetComponent<Card>().cardAttack >= o.GetComponent<Card>().cardHealth)
                                    if (x.GetComponent<CardController>().target == null)
                                    {
                                        x.GetComponent<CardController>().target = o.gameObject;
                                        break;
                                    }
                                        

                                if(x.GetComponent<Card>().cardAttack <= o.GetComponent<Card>().cardHealth)
                                {
                                    int r = Random.Range(0, 2);
                                    if (x.GetComponent<CardController>().target == null && r > 0)
                                    {
                                        x.GetComponent<CardController>().target = o.gameObject;
                                        break;
                                    }
                                       
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var x in enemyCardsOnField)
                        {
                            if (x.GetComponent<Card>().cardAttack >= o.GetComponent<Card>().cardHealth)
                                if (x.GetComponent<CardController>().target == null)
                                {
                                    x.GetComponent<CardController>().target = o.gameObject;
                                    break;
                                }
                                    

                            if (x.GetComponent<Card>().cardAttack <= o.GetComponent<Card>().cardHealth)
                            {
                                int r = Random.Range(0, 2);
                                if (x.GetComponent<CardController>().target == null && r > 0)
                                {
                                    x.GetComponent<CardController>().target = o.gameObject;
                                    break;
                                }
                                    
                            }
                        }
                    }
                }
            }
            else
            {
                foreach(var o in playerCardsOnField)
                {
                    if (o.GetComponent<CardController>().isFlying)
                    {
                        foreach(var x in enemyCardsOnField)
                        {
                            if(x.GetComponent<CardController>().isFlying || x.GetComponent<CardController>().isArcher)
                            {
                                if (x.GetComponent<Card>().cardAttack >= o.GetComponent<Card>().cardHealth)
                                    if (x.GetComponent<CardController>().target == null)
                                    {
                                        x.GetComponent<CardController>().target = o.gameObject;
                                        break;
                                    }
                                        
                            }
                        }
                    }
                    else
                    {
                        foreach (var x in enemyCardsOnField)
                        {
                            if (x.GetComponent<Card>().cardAttack >= o.GetComponent<Card>().cardHealth)
                                if (x.GetComponent<CardController>().target == null)
                                {
                                    x.GetComponent<CardController>().target = o.gameObject;
                                    break;
                                }
                                   
                        }
                    }
                }
            }
        }
    }

    public void EnemyMainfaceLogic()
    {
        GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
        GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
        GameObject hand = GameObject.Find("EnemyHandZone");

        List<GameObject> cardsInHand = new List<GameObject>();
        List<GameObject> playerCardsOnField = new List<GameObject>();
        List<GameObject> enemyCardsOnField = new List<GameObject>();

        for (var i = 0; i < hand.transform.childCount; i++)
            cardsInHand.Add(hand.transform.GetChild(i).gameObject);

        foreach(var o in playerCards)
        {
            if (o.GetComponent<CardController>().isOnGameField && o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                playerCardsOnField.Add(o);
        }

        foreach (var o in enemyCards)
        {
            if (o.GetComponent<CardController>().isOnGameField && o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                enemyCardsOnField.Add(o);
        }

        foreach (var o in cardsInHand)
        {
            if (enemyMana >= o.GetComponent<Card>().cardCost)
            {
                if (o.GetComponent<Card>().cardName == "Kataklizm" || o.GetComponent<Card>().cardName == "Deszcz Meteorytów" || o.GetComponent<Card>().cardName == "Lodowe pociski")
                {
                    if (playerCardsOnField.Count >= 3 && enemyCardsOnField.Count < 2)
                    {
                        PlayCard(o);
                    }
                }

                if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                    PlayCard(o);

                if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_ARTIFACT && GameObject.Find("EnemyArtifact").transform.childCount < 1)
                    PlayCard(o);

                if (o.GetComponent<Card>().cardName == "Śmiertelna kołysanka")
                {
                    if (playerCardsOnField.Count > 0)
                    {
                        GameObject highest = playerCardsOnField[0].gameObject;
                        foreach (var x in playerCardsOnField)
                        {
                            if (x.GetComponent<Card>().cardHealth > highest.GetComponent<Card>().cardHealth)
                                highest = x.gameObject;
                        }
                        o.GetComponent<CardController>().target = highest.gameObject;
                    }
                }

                if (o.GetComponent<Card>().cardName == "Ognisty podmuch" || o.GetComponent<Card>().cardName == "Latające noże")
                    PlayCard(o);
            }
        }
    }

    private void PlayCard(GameObject o)
    {
        if (o.GetComponent<CardController>().isArtifact)
        {
            if (GameObject.Find("EnemyArtifact").transform.childCount < 1)
            {
                o.transform.SetParent(GameObject.Find("EnemyArtifact").transform);
                o.GetComponent<Card>().isCovered = false;
                o.GetComponent<CardController>().isOnGameField = true;
                o.GetComponent<CardController>().summoningSickness = true;
                enemyMana -= o.GetComponent<Card>().cardCost;
            }
            else
                return;
        }
        else if(o.GetComponent<CardController>().isMonster)
        {
            if (GameObject.Find("EnemyField").transform.childCount < 8)
            {
                o.transform.SetParent(GameObject.Find("EnemyField").transform);
                o.GetComponent<Card>().isCovered = false;
                o.GetComponent<CardController>().summoningSickness = true;
                o.GetComponent<CardController>().isAttacking = false;
                enemyMana -= o.GetComponent<Card>().cardCost;
            }
            else
                return;
        }
        else
        {
            o.transform.SetParent(GameObject.Find("PlayedSpellDisplay").transform);
            o.GetComponent<Card>().isCovered = false;
            o.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            enemyMana -= o.GetComponent<Card>().cardCost;
        }
    }

    public void selectAttack(GameObject o)
    {
        if (!o.GetComponent<CardController>().summoningSickness)
        {
            o.GetComponent<SelectCard>().playerCard = o;
            o.GetComponent<SelectCard>().currentScale = o.transform.localScale;
            o.transform.localScale = new Vector3(1, 1, 1);
            o.GetComponent<CardController>().isAttacking = true;
        }
    }
}

