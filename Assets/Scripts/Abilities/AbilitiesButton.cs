using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AbilitiesButton : MonoBehaviour
{
    public Ability[] abilities;
    
    public string buttonName;
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        abilities = new Ability[3];
        abilities[0] = null;
        abilities[1] = null;
        abilities[2] = null;
    }

    public Ability[] getAbilityButton(){
        return abilities;
    }
    public string getButtonName(){
        return buttonName;
    }
    public void addAbilityToAbilities(Ability ability){
        for(int i = 0; i < abilities.Length; i++){
            if(abilities[i] == null && !abilities.Contains(ability)){
                abilities[i] = ability;
                break;
            }
        }
    }
    public void removeAbilityFromAbilities(Ability ability){
        for(int i = 0; i < abilities.Length; i++){
            if(abilities[i].Equals(ability)){
                abilities[i] = null;
            }
        }
    }
    public void removeAbilityFromAbilitiesByNameGameObject(GameObject gameObject){
        string skillName = gameObject.transform.name;

        for(int i = 0; i < abilities.Length; i++){
            if(abilities[i] != null){
                if(abilities[i].getName().Equals(skillName)){
                    abilities[i] = null;
                    gameManager.GetUI().updateToolbarInterface();
                    break;
                }
            }
        }
    }

}
