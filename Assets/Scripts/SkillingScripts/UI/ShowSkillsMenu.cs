using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkillsMenu : MonoBehaviour
{
    GameManager gameManager;
    List<Skill> skills;
    public GameObject skillSlot;
    public bool instatiated;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnEnable()
    {
        StartCoroutine(updateSkillsWait());
    }
    public IEnumerator updateSkillsWait(){
        yield return new WaitForSeconds(0.1f);
        updateSkills();
    }
    
    public void updateSkills(){
        skills = 
        skills = gameManager.getPlayerBehavior().getSkills().GetSkills();
        int count = 0;

        if(!instatiated && skills.Count > 0){
            instatiated = true;
            for(int i = 0; i < skills.Count; i++){
                Instantiate(skillSlot, transform.Find("Background/Skills"));
            }
        }

        foreach(Skill skill in skills){
            transform.Find("Background/Skills").transform.GetChild(count).transform.Find("skillName").GetComponent<Text>().text = skill.getName();
            transform.Find("Background/Skills").transform.GetChild(count).transform.Find("level").GetComponent<Text>().text = skill.getLevel().ToString();
            transform.Find("Background/Skills").transform.GetChild(count).transform.Find("Experience Panel/Exp").GetComponent<Text>().text = skill.getExperience().ToString();
            transform.Find("Background/Skills").transform.GetChild(count).transform.Find("Experience Panel/Exp Next Lvl").GetComponent<Text>().text = skill.getExperienceNextLevel().ToString();
            transform.Find("Background/Skills").transform.GetChild(count).transform.Find("Experience Panel/Exp Til Next").GetComponent<Text>().text = skill.getExperienceLeft().ToString();
            transform.Find("Background/Skills").transform.GetChild(count).transform.Find("Experience Panel/Percentage").GetComponent<Text>().text = skill.getPercentageLeft().ToString("0.##") + "%";   
            count++;           
        }
    }
}
