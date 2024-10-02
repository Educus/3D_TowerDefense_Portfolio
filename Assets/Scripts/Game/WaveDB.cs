using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDB : MonoBehaviour
{
    enum EnemyType
    {
        WalkMushroomSmile,  // 0
        RunMushroomSmile,   // 1
        WalkMushroomAngry,  // 2
        RunMushroomAngry,   // 3
        WalkGrunt,          // 4
        RunGrunt,           // 5
        WalkCactus,         // 6
        RunCatus,           // 7
        Golem               // 8
    }

    public string[] waveEnemy = // enemyType, enemySirialNumber1, enemyNumber1, SirialNumber2, Number2 ....
        {
        "1,0,5", // Desert 1round 1wave
        "1,1,5", // Desert 1round 2wave
        "2,0,6,1,4", // Desert 1round 3wave
        "2,0,10,1,8", // Desert 1round 4wave
        "2,0,15,1,10", // Desert 1round 5wave
        "1,2,5", // Desert 2round 1wave
        "1,3,5", // Desert 2round 2wave
        "2,2,6,3,4", // Desert 2round 3wave
        "2,2,10,3,8", // Desert 2round 4wave
        "2,2,15,3,10", // Desert 2round 5wave
        "1,4,5", // Desert 3round 1wave
        "1,5,5", // Desert 3round 2wave
        "2,4,6,5,4", // Desert 3round 3wave
        "2,4,10,5,8", // Desert 3round 4wave
        "2,6,10,7,8", // Desert 3round 5wave
        "1,4,6", // Desert 4round 1wave
        "2,4,6,5,3", // Desert 4round 2wave
        "1,6,16", // Desert 4round 3wave
        "1,8,1", // Desert 4round 4wave
        "2,7,3,8,1", // Desert 4round 5wave
        "2,0,6,1,6", // Desert 5round 1wave
        "3,2,5,3,6,4,7", // Desert 5round 2wave
        "3,4,8,5,6,4,8", // Desert 5round 3wave
        "3,6,8,7,6,6,8", // Desert 5round 4wave
        "1,8,2", // Desert 5round 5wave
        "3,1,4,0,8,3,4", // Desert 6round 1wave
        "4,0,6,2,6,4,6,6,4", // Desert 6round 2wave
        "2,4,6,5,10", // Desert 6round 3wave
        "2,6,10,8,1", // Desert 6round 4wave
        "2,7,6,8,2", // Desert 6round 5wave

        "1,0,5", // Forest 1round 1wave
        "1,1,5", // Forest 1round 2wave
        "2,0,6,1,4", // Forest 1round 3wave
        "2,0,10,1,8", // Forest 1round 4wave
        "2,0,15,1,10", // Forest 1round 5wave
        "1,2,5", // Forest 2round 1wave
        "1,3,5", // Forest 2round 2wave
        "2,2,6,3,4", // Forest 2round 3wave
        "2,2,10,3,8", // Forest 2round 4wave
        "2,2,15,3,10", // Forest 2round 5wave
        "1,4,5", // Forest 3round 1wave
        "1,5,5", // Forest 3round 2wave
        "2,4,6,5,4", // Forest 3round 3wave
        "2,4,10,5,8", // Forest 3round 4wave
        "2,6,10,7,8", // Forest 3round 5wave
        "1,4,6", // Forest 4round 1wave
        "2,4,6,5,3", // Forest 4round 2wave
        "1,6,16", // Forest 4round 3wave
        "1,8,1", // Forest 4round 4wave
        "2,7,3,8,1", // Forest 4round 5wave
        "2,0,6,1,6", // Forest 5round 1wave
        "3,2,5,3,6,4,7", // Forest 5round 2wave
        "3,4,8,5,6,4,8", // Forest 5round 3wave
        "3,6,8,7,6,6,8", // Forest 5round 4wave
        "1,8,2", // Forest 5round 5wave
        "3,1,4,0,8,3,4", // Forest 6round 1wave
        "4,0,6,2,6,4,6,6,4", // Forest 6round 2wave
        "2,4,6,5,10", // Forest 6round 3wave
        "2,6,10,8,1", // Forest 6round 4wave
        "2,7,6,8,2", // Forest 6round 5wave

        "1,0,5", // Winter 1round 1wave
        "1,1,5", // Winter 1round 2wave
        "2,0,6,1,4", // Winter 1round 3wave
        "2,0,10,1,8", // Winter 1round 4wave
        "2,0,15,1,10", // Winter 1round 5wave
        "1,2,5", // Winter 2round 1wave
        "1,3,5", // Winter 2round 2wave
        "2,2,6,3,4", // Winter 2round 3wave
        "2,2,10,3,8", // Winter 2round 4wave
        "2,2,15,3,10", // Winter 2round 5wave
        "1,4,5", // Winter 3round 1wave
        "1,5,5", // Winter 3round 2wave
        "2,4,6,5,4", // Winter 3round 3wave
        "2,4,10,5,8", // Winter 3round 4wave
        "2,6,10,7,8", // Winter 3round 5wave
        "1,4,6", // Winter 4round 1wave
        "2,4,6,5,3", // Winter 4round 2wave
        "1,6,16", // Winter 4round 3wave
        "1,8,1", // Winter 4round 4wave
        "2,7,3,8,1", // Winter 4round 5wave
        "2,0,6,1,6", // Winter 5round 1wave
        "3,2,5,3,6,4,7", // Winter 5round 2wave
        "3,4,8,5,6,4,8", // Winter 5round 3wave
        "3,6,8,7,6,6,8", // Winter 5round 4wave
        "1,8,2", // Winter 5round 5wave

        "1,0,1",    // Test
        "1,0,1",    // Test
        "1,0,1",    // Test
        "1,0,1",    // Test
        "1,0,1"     // Test

        // "3,1,4,0,8,3,4", // Winter 6round 1wave
        // "4,0,6,2,6,4,6,6,4", // Winter 6round 2wave
        // "2,4,6,5,10", // Winter 6round 3wave
        // "2,6,10,8,1", // Winter 6round 4wave
        // "2,7,6,8,2", // Winter 6round 5wave
    };
}
