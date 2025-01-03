using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkillManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Basic_Skill_None;
    public GameObject Basic_Skill_Fire;
    public GameObject Basic_Skill_Sword;
    public GameObject LargeMeteor;
    public GameObject MediumMeteor;
    public GameObject SmallMeteor;
    public GameObject Lightning;
    public List<GameObject> Special_Skill_One;
    public List<GameObject> Special_Skill_Two;
    [SerializeField] public Animator animator;
    private bool isUpdatedS1;
    private bool isUpdatedS2;
    private int cooldownS4;
    private int cooldownS5;
    private AudioManager audioManager;
    private EffectManagement effectManagement;
    void Awake()
    {
        effectManagement = GameObject.FindGameObjectWithTag("BattleEffect").GetComponent<EffectManagement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        cooldownS4 = 0;
        cooldownS5 = 0;
        isUpdatedS1 = false;
        isUpdatedS2 = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        resetSkillIcon();
        if (isUpdatedS1)
        {
            handleCooldownOnGameScreenS1();
            isUpdatedS1 = false;
        }
        if (isUpdatedS2)
        {
            handleCooldownOnGameScreenS2();
            isUpdatedS2 = false;
        }
        if (Input.GetButtonDown("Skill4"))
        {
            if (cooldownS4 == 0)
            {
                cooldownS4 = 8;
                isUpdatedS1 = true;
                Skill4();
            }
            else
            {
                Debug.Log("Chờ hồi chiêu");
            }
        }
        if (Input.GetButtonDown("Skill5"))
        {
            if (cooldownS5 == 0)
            {
                cooldownS5 = 8;
                isUpdatedS2 = true;
                Skill5();
            }
            else
            {
                Debug.Log("Chờ hồi chiêu");
            }
        }
    }
    IEnumerator ExecuteS1AfterTime(float time)
    {
        // Chờ trong khoảng thời gian đã định (0.5s trong trường hợp này)
        yield return new WaitForSeconds(time);

        // Sau khi hết thời gian, thực hiện hành động ActionA()
        if (cooldownS4 > 0)
        {
            cooldownS4 -= 1;
        }
        isUpdatedS1 = true;
    }

    IEnumerator ExecuteS2AfterTime(float time)
    {
        // Chờ trong khoảng thời gian đã định (0.5s trong trường hợp này)
        yield return new WaitForSeconds(time);

        // Sau khi hết thời gian, thực hiện hành động ActionA()
        if (cooldownS5 > 0)
        {
            cooldownS5 -= 1;
        }
        isUpdatedS2 = true;
    }

    private void handleCooldownOnGameScreenS1()
    {
        StartCoroutine(ExecuteS1AfterTime(0.5f));
        resetSkillIconCoolDownS1();
        int index_1 = (16 - (cooldownS4 * 2)) / 2;
        Special_Skill_One[index_1].SetActive(true);
    }

    private void handleCooldownOnGameScreenS2()
    {
        StartCoroutine(ExecuteS2AfterTime(0.5f));
        resetSkillIconCoolDownS2();
        int index_2 = (16 - (cooldownS5 * 2)) / 2;
        Special_Skill_Two[index_2].SetActive(true);
    }

    private void resetSkillIconCoolDownS1()
    {
        foreach (var frame in Special_Skill_One)
        {
            frame.SetActive(false);
        }
    }

    private void resetSkillIconCoolDownS2()
    {
        foreach (var frame in Special_Skill_Two)
        {
            frame.SetActive(false);
        }
    }

    public void resetSkillIcon()
    {
        Basic_Skill_Fire.SetActive(false);
        Basic_Skill_None.SetActive(false);
        Basic_Skill_Sword.SetActive(false);

        if (animator.GetBool("isNoneState") == true)
        {
            Basic_Skill_None.SetActive(true);
        }
        else if (animator.GetBool("isElementState") == true)
        {
            Basic_Skill_Fire.SetActive(true);
        }
        else
        {
            Basic_Skill_Sword.SetActive(true);
        }
    }

    private void Skill4()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        for (float i = -20f; i <= 20f; i += 2f)
        {
            for (float j = 10f; j <= 20f; j += 2f)
            {
                audioManager.PlaySFX(audioManager.meteoFallingDown);
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    Instantiate(LargeMeteor, new Vector3(x + i, y + j, 0f), Quaternion.identity);
                }
                else if (r == 1)
                {
                    Instantiate(MediumMeteor, new Vector3(x + i, y + j, 0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(SmallMeteor, new Vector3(x + i, y + j, 0f), Quaternion.identity);
                }
            }
        }
    }

    private void Skill5()
    {
        effectManagement.ShakeIt(2.0f);
        StartCoroutine(SpawnLightning());
    }

    IEnumerator SpawnLightning()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        for (int i = 0; i < 10; i++)
        {
            float distanceX = Random.Range(-20f, 20f); // x ngẫu nhiên trong khoảng cố định

            // Instantiate tia sét tại vị trí x ngẫu nhiên và y = 2
            GameObject A = Instantiate(Lightning, new Vector3(x + distanceX, y + 4.0f, 0f), Quaternion.identity);
            AudioClip lightning = audioManager.lightning;
            audioManager.PlaySFX(audioManager.lightning);
            // Chờ 0.5 giây trước khi spawn tia sét tiếp theo
            yield return new WaitForSeconds(0.2f);
        }
        // effectManagement.StopCameraShaking();
    }
}
