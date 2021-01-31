using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts {
    public class Node : MonoBehaviour
    {
        [SerializeField]
        int Rank = 0;
        
        [SerializeField]
        GameObject[] Neighbours;


        private Region region;

        public int getRank(){
            return region.getRank();
        }

        public void setRank(int aRank){
            Rank = aRank;
        }

        public void setRegion(Region r){
            region = r;
            Rank = region.getRank();
        }

        public Region GetRegion(){
            return region;
        }

        public GameObject getLowestRankNeighbour(){
            int lowestRank = Rank+1;
            GameObject LowestRanker = null;
            foreach(GameObject n in Neighbours){
                Debug.Log(lowestRank);
                if(n.GetComponent<Node>().getRank() < lowestRank){
                    lowestRank = n.GetComponent<Node>().getRank();
                    Debug.Log(lowestRank);
                    LowestRanker = n;
                }
            }

            return LowestRanker;
        }

        public List<int> pathFinder(int aRank, int timeleft, List<int> visited){
            //check if out of time
            if(timeleft < 0){
                List<int> path = new List<int>();
                path.Add(-1);
                return path;
            }

            visited.Add(Rank);
            //check if we found path
            if(Rank == aRank){
                List<int> path = new List<int>();
                path.Add(Rank);
                return path;
            }
            //find shortest path
            List<List<int>> allpaths = new List<List<int>>();
            foreach(GameObject n in Neighbours){
                if(visited.Contains(n.GetComponent<Node>().getRank())){
                    continue;
                }
                List<int> path = new List<int>();
                path.Add(Rank);
                List<int> fromNode = n.GetComponent<Node>().pathFinder(aRank, timeleft-1, visited);
                if(fromNode == null){
                    continue;
                }
                path.AddRange(fromNode);
                if(path[path.Count -1] == -1){
                    continue;
                }
                allpaths.Add(path);
            }

            int shortestSize = 11;
            int shortest = -1;
            for(int i = 0; i<allpaths.Count; i++){
                if(allpaths[i].Count < shortestSize){
                    shortestSize = allpaths[i].Count;
                    shortest = i;
                }
            }
            if(shortest == -1){
                return null;
            }
            return allpaths[shortest];
        }

        public GameObject[] getNeighbours(){
            return Neighbours;
        }



        

        

        void OnMouseDown() {
            MoveButton moveButton = FindObjectOfType<MoveButton>();
            ThoraldMoveButton thoraldButton = FindObjectOfType<ThoraldMoveButton>();
            //If move button was pressed, move hero
            if(moveButton.Pressed)
            {   
                GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
                GameObject hero = GameObject.Find(GM.GetHeroByPID(HeroSelection.HS.mySelectedCharacter));
                hero.GetComponent<HeroController>().heroMove(this.gameObject);
                moveButton.Pressed = false;
            }
            //If thorald button pressed, move thorald
            else if(thoraldButton.Pressed)
            {
                FindObjectOfType<ThoraldController>().Move(gameObject);
            }
        }
    }
}