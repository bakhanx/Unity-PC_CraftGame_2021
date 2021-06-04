# Unity-PC_CraftGame

## Title : 미정 (진행중)

> - 장르 : 생존, 크래프트 <br>
> - 개발 : 1인 개발 <br>
> - 플랫폼 : PC <br>
> - 유니티 버젼 : 2020.3.2f1 <br>
> - 제작기간 : 진행중

## Controller

- Player Controller
  - Walk
  - Run
  - Crouch
  - Jump
  - Camera Rotation
  - Character Rotation

- Action Controller
  - Pick Up
  - Check Item
  - Item Info Appear

- Gun Controller
  - Fire
  - Reload
  - FineSightMode
  - GunChange

- Hand Controller
  - Swing

- Axe Controller
  - Swing
  - Mining

- ~~PickAxe Controller (미구현)~~

<br>

- Close Weapon Controller (근접무기 상속)
  - Hand Controller
  - Axe Controller
  - ~~PickAxe (미구현)~~

## Weapon Manager
- Change Weapon
- Weapon[] Controller

## AI (NPC)
- View Angle
- Animal
  - Common 
    - Move
    - Rotation
    - Reset
    - Random Action
    - Random Sound
    - Interaction
      - Damage
      - Dead
  - Strong
    - ~~Tiger (미구현)~~
  - Weak
    - Common 
      - Run (Hurt, in ViewAngle)
    - Pig 
      - Wait
      - Eat
      - Peek
      - Walk

## Item
- Gun
- Potion

## UI
- Inventory
- Slot
  - Drag Slot
- Status Controller
  - HP
  - DP
  - SP
  - Hungry
  - Thirsty
  - Satisfy
