using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour{
    public static LevelGenerator sharedInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>(); //todos los bloques del nivel
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>(); //los bloques que estan en la pantalla
    public Transform levelInitialPoint; //punto de partida del nivel
    private bool isGeneratingInitialBlocks = false;

    void Awake(){
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start(){
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void GenerateInitialBlocks(){
        isGeneratingInitialBlocks = true;
        for(int i = 0; i < 3; i++){
            AddNewBlock();
        }
        isGeneratingInitialBlocks = false;
    }

    public void AddNewBlock(){
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);
        if(isGeneratingInitialBlocks) randomIndex = 0;
        //Instantiate copia del objeto que esta en la lista de objetos
        LevelBlock block = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
        //Mantener el mismo padre
        block.transform.SetParent(this.transform, false);

        //Posicion del bloque
        Vector3 blockPosition = Vector3.zero;
        if(currentLevelBlocks.Count == 0){
            //Primer bloque
            blockPosition = levelInitialPoint.position;
        }else{
            //Bloque siguiente
            blockPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }
        block.transform.position = blockPosition;
        currentLevelBlocks.Add(block);
    }

    public void RemoveOldBlock(){
        //Eliminar el ultimo bloque de la lista
        LevelBlock block = currentLevelBlocks[0];
        currentLevelBlocks.Remove(block);
        //Destruir el bloque
        Destroy(block.gameObject);
    }

    public void RemoveAllTheBlocks(){
        while(currentLevelBlocks.Count > 0){
            RemoveOldBlock();
        }
    }

}
