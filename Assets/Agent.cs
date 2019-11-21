using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private int k = 3;
    [SerializeField]
    private float lr = 0.1f;
    [SerializeField]
    private float exp_rate = 0.3f;
    [SerializeField]
    private bool ucb = true;
    [SerializeField]
    private float c = 2f;
    [SerializeField]
    private float time_lag = 0.1f;
    List<int> actions = new List<int>();
    public float total_reward = 0;
    List<float> avg_reward = new List<float>();
    List<float> true_val = new List<float>();
    System.Random rnd = new System.Random();
    private float time = 0.0f;
    List<float> values = new List<float>();
    List<float> action_times = new List<float>();
    List<float> confidence_int = new List<float>();
    // Start is called before the first frame update
    [SerializeField]
    private GameObject g0, g1, g2;
    void Start()
    {
        g0 = GameObject.Find("zero");
        g1 = GameObject.Find("one");
        g2 = GameObject.Find("two");
        g0.SetActive(false);
        g1.SetActive(false);
        g2.SetActive(false);
        for (int i = 0; i < k; i++)
        {
            actions.Add(i);
        }

        for (int i = 0; i < k; i++)
        {
            true_val.Add((float)Random.Range(0f, 1f));
        }


        for (int i = 0; i < k; i++)
        {
            values.Add(0f);
        }
        for (int i = 0; i < k; i++)
        {
            action_times.Add(0f);
        }

        StartCoroutine(execute());
    }
    public int chooseaction(float exp_rate, float c, List<float> values, List<float> action_times, List<int> actions, float time, bool ucb)
    {
        int action = 0;
        if ((float)Random.Range(0.0f, 1.0f) <= exp_rate)
        {
            int idx = rnd.Next(actions.Count);
            action = idx;


        }

        else
        {
            if (ucb)
            {
                if (time == 0f)
                {
                    action = rnd.Next(actions.Count);

                }
                else
                {

                    for (int i = 0; i < k; i++)
                    {
                        float x = (float)values[i];
                        float r = c * (Mathf.Sqrt(Mathf.Log((float)time) / (action_times[i] + 0.1f)));
                        x += r;
                        confidence_int[i] = (float)x;

                    }
                    float max = 9999f;
                    action = 0;
                    for (int j = 0; j < k; j++)
                    {
                        if (confidence_int[j] > max)
                        {
                            max = confidence_int[j];
                            action = j;
                        }
                    }


                }
            }
            else
            {
                float maxi = 9999f;
                for (int j = 0; j < k; j++)
                {
                    if (values[j] > maxi)
                    {
                        maxi = values[j];
                        action = j;
                    }

                }
            }

        }

        return action;

    }

    public void takeaction(float total_reward, List<float> avg_reward, List<float> action_times, float time, int action, List<float> true_val)
    {
        time++;
        action_times[action] += 1;
        float reward = Random.Range(0f, 1.0f) + true_val[action];
        values[action] += lr * (reward - values[action]);
        total_reward += reward;
        avg_reward.Add(total_reward / time);

    }

    public void play(int n)
    {
        for (int y = 0; y < n; y++)
        {
            int act = chooseaction(exp_rate, c, values, action_times, actions, time, ucb);
            takeaction(total_reward, avg_reward, action_times, time, act, true_val);
        }
    }

    IEnumerator execute()
    {
        yield return new WaitForSeconds(time_lag);

        play(900);
        //      Debug.Log("Total REward " + total_reward);
        /*for (int t = 0; t < k; t++)
        {
            Debug.Log("True Values " + true_val[t]);
            Debug.Log("Estimated values " + values[t]);

        }
*/
        float thresh = 0.9f;
        for (int y = 0; y < avg_reward.Count; y++)
        {
            if (avg_reward[y] > thresh)
            {
                Debug.Log("select " + y + y % 3);
                if (y % 3 == 0)
                {
                    g0.SetActive(true);
                    g1.SetActive(false);
                    g2.SetActive(false);
                }
                else if (y % 3 == 1)
                {
                    g0.SetActive(false);
                    g1.SetActive(true);
                    g0.SetActive(false);
                }
                else if (y % 3 == 2)
                {
                    g0.SetActive(false);
                    g1.SetActive(false);
                    g2.SetActive(true);
                }
            }
            yield return new WaitForSeconds(time_lag);
        }




    }

    // Update is called once per frame
    void Update()
    {

    }

}
