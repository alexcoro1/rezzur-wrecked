using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

// by Alexandru Corodati

namespace Rezzur_Wrecked
{
    public class Sound
    {
        private SoundEffectInstance s_effect_instance; // example to remember
        public SoundEffectInstance player_walk;
        public SoundEffectInstance player_attack;
        public SoundEffectInstance player_jump;
        public SoundEffectInstance enemy_hover;
        public SoundEffectInstance enemy_attack;

        public  void Initialize(SoundEffect pwalk, SoundEffect patt, SoundEffect pjump, SoundEffect enemh, SoundEffect enema)
        {
            //s_effect_instance = s_effect.CreateInstance();
            player_walk = pwalk.CreateInstance();
            player_attack = patt.CreateInstance();
            player_jump = pjump.CreateInstance();
            enemy_hover = enemh.CreateInstance();
            enemy_attack = enema.CreateInstance();
        }

        public SoundEffectInstance S_EFFECT
        {
            get { return s_effect_instance; }
        }
        public SoundEffectInstance P_WALK
        {
            get { return player_walk; }
        }
        public SoundEffectInstance P_ATT
        {
            get { return player_attack; }
        }
        public SoundEffectInstance P_JUMP
        {
            get { return player_jump; }
        }
        public SoundEffectInstance E_HOV
        {
            get { return enemy_hover; }
        }
        public SoundEffectInstance E_ATT
        {
            get { return enemy_attack; }
        }
        
    }

}
