using System;
using UnityEngine;

public class DatableEntity : MonoBehaviour
{
    void Start()
    {
        DatableEntity[] entities=new DatableEntity[10];
        entities[0] = new DatableEntity("King Circle");
        entities[1] = new DatableEntity("Pink Square");
        entities[2] = new DatableEntity("Blue Square");
        entities[3] = new DatableEntity("Green Square");
    }
    private string entityName;

    private int affection;
    private Boolean ending;

    public DatableEntity()
    {
        this.entityName = "Default entityName";
        this.affection = 0;
    }

    public DatableEntity(string entityName)
    {
        this.entityName = entityName;
        this.affection = 0;
        this.ending = false;
    }

    public DatableEntity(string entityName, int affection)
    {
        this.entityName = entityName;
        this.affection = affection;
        this.ending = false;
    }

    public String getEntityName()
    {
        return this.entityName;
    }

    public int getAffection()
    {
        return this.affection;
    }

    public void setAffection(int affection)
    {
        this.affection = affection;
    }

    public Boolean getEnding()
    {
        return this.ending;
    }

    public void setEnding(Boolean ending)
    {
        this.ending = ending;
    }
}