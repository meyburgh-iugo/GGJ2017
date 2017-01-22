using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMessageManager : MonoBehaviour
{
  private Transform player;

  private string[][] TotallyRandomMessage;
  private List<int> TotallyRandomIndexes;

  private string[][] MilestoneMessage;
  private List<bool>  MilestoneIndexes;

	// Use this for initialization
	void Start ()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    TotallyRandomMessage = new string[28][];

    TotallyRandomMessage[0] = new string[] { "Well, isn't this a fine kettle of fish?!" };
    TotallyRandomMessage[1] = new string[] { "Donkey Kong is not even a donkey." };
    TotallyRandomMessage[2] = new string[] { "We swear it sounded cool at the first time."};
    TotallyRandomMessage[3] = new string[] { "I could use some ice cream pizza." };
    TotallyRandomMessage[4] = new string[] { "Hey, player. Please don't make me die." };
    TotallyRandomMessage[5] = new string[] { "Is that a bug?...", " Let me see the specs." };
    TotallyRandomMessage[6] = new string[] { "Stupid free tool...", "I should send them a letter asking a custom version."};
    TotallyRandomMessage[7] = new string[] { "Hey, player!...", "Leave the keyboard alone." };
    TotallyRandomMessage[8] = new string[] { "Hey, you know, it is fun...", "We should do it every weekend." };
    TotallyRandomMessage[9] = new string[] { "It should be fun, man...", "But I am depressed now." };
    TotallyRandomMessage[10] = new string[] { "You know. I don't understand...", "How can I die by a jellyfish...", "being inside a whole steel diving machine?"};
    TotallyRandomMessage[11] = new string[] { "You know What?...", "Ahh, nevermind." };
    TotallyRandomMessage[12] = new string[] { "Hey, Georgie...", "Don't you want a balloon?" };
    TotallyRandomMessage[13] = new string[] { "Permission to Speak, Sir!" };
    TotallyRandomMessage[14] = new string[] { "Perdon me...", "But, are you bored?" };
    TotallyRandomMessage[15] = new string[] { "Tell me the truth...", "Do you like our game?" };
    TotallyRandomMessage[16] = new string[] { "This game is legen...", " wait for it...", "...dary." };
    TotallyRandomMessage[17] = new string[] { "OH MY GOD! THEY KILLED KENNY! YOU BASTARDS!" };
    TotallyRandomMessage[18] = new string[] { "You're playing so well...", "That you're fired!" };
    TotallyRandomMessage[19] = new string[] { "19, huh?", "Cool, I prime number." };
    TotallyRandomMessage[20] = new string[] { "What's up, player?" };
    TotallyRandomMessage[21] = new string[] { "I have a bad feeling about this" };
    TotallyRandomMessage[22] = new string[] { "It is the Demogorgon." };
    TotallyRandomMessage[23] = new string[] { "Release the Kraken!" };
    TotallyRandomMessage[24] = new string[] { "If the game has no bugs...", "It would be boring" };
    TotallyRandomMessage[25] = new string[] { "Yoh, mah player, keep it random." };
    TotallyRandomMessage[26] = new string[] { "The bar needs to be raised!...", "No one appreciates what I'm trying to do!" };
    TotallyRandomMessage[27] = new string[] { "Do you like fish sticks?"};

    TotallyRandomIndexes = new List<int>() {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27};

    MilestoneMessage = new string[1][];
    MilestoneMessage[0] = new string[] { "Yo, player. You should be going down...", "There is nothing up here, just painful death" };

    MilestoneIndexes = new List<bool>() { true };

    StartCoroutine(PickRandomMessage());
  }
	
	// Update is called once per frame
	void Update ()
  {
		if (player.position.y > 100 && MilestoneIndexes[0])
    {
      MilestoneIndexes[0] = false;
      for (int i = 0; i < MilestoneMessage[0].Length; i++)
      {
        MessagePooler.Instance.QueueMessage(MilestoneMessage[0][i]);
      }
    }
	}

  IEnumerator PickRandomMessage()
  {
    yield return new WaitForSeconds(Random.Range(15, 35));

    while (TotallyRandomMessage.Length > 0)
    {
      int idx = Random.Range(0, TotallyRandomIndexes.Count);
      for (int i = 0; i < TotallyRandomMessage[TotallyRandomIndexes[idx]].Length; i++)
      {
        MessagePooler.Instance.QueueMessage(TotallyRandomMessage[TotallyRandomIndexes[idx]][i]);
      }
      TotallyRandomIndexes.Remove(idx);

      yield return new WaitForSeconds(Random.Range(15, 45));
    }
  }
}
