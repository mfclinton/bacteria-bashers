# Bacteria Bashers

You control a swarm of tiny bacteria and fight enemies much bigger than you. Hitting them knocks loose more bacteria allies, and your swarm grows as you fight. It works in a browser and on mobile.

- Play: [itch.io](https://unitedfailures.itch.io/bacteria-bashers)
- Made: October 2024 for Ludum Dare 56
- Team: [@mfclinton](https://github.com/mfclinton) (programming), [@DavidKoleczek](https://github.com/DavidKoleczek) (programming), [Blue](https://sovereignblue.artstation.com/) (art), [@MrAozora](https://github.com/MrAozora) (music and audio)
- Credits: Poisson disk sampling from a3geek's [Fast Poisson Disk Sampling for Unity](https://gist.github.com/a3geek/8532817159b77c727040cf67c92af322) gist
- Engine: Unity, C#

This is an export of a private repo with only the code we wrote. Art, audio, the Unity project files, and third party plugins aren't included. The history is squashed into one commit.

## What I built

- The swarm behavior. You move a control zone around, and each blob drifts toward its own spot in it, using Perlin noise seeded per blob. Once an enemy is in range, the blobs spread out around it before charging in.
- Enemy spawning, with difficulty curves I set up for tuning that scale each enemy's health, speed, and attack timing. Enemies wind up before they attack and nudge other enemies out of their way.
- Damaging an enemy has a chance to spawn new blobs that join your swarm.
- Blobs and enemies share one entity base and are built from separate behavior components for moving and attacking.
- A game state machine and events for attacks, damage, and spawning, behind small subscription interfaces. The gameplay code never references audio or UI. Menus, score tracking, and the FMOD music and sound effects each subscribe to just the events they need.
- Health bars drawn by a shader. They all share one material, and each bar's fill is set through a MaterialPropertyBlock.
