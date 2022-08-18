# BoarGame

Ideas

- **Gun**

sprite2d _sprite;<br />
float _damage;<br />
float _fireRate;<br />
float _reloadSpeed;<br />
float _accuracy;<br />
float _range;<br />
int _ammo = 300;<br />
float _durability;<br />
enum _fireMode{ Automatic, ...}<br />
enum _ammoType{ Shootgun, Pistol, Riffle, Granades, Electric?}<br />

_//When the player is out of ammo, automaticaly throws the weapon / if he presses the throw key_<br />
void Throw();<br />
void Fire();<br />
void Reload();<br />
void Attack();<br />

- **Granade**

sprite2d _sprite;<br />
float _damage;<br />
float _range;<br />
int _ammo;<br />

void Throw();<br />
void Fire();<br />
void Explode();<br />

- **Melee Attacks**

sprite2d _sprite;<br />
float _damage;<br />
float _durability;<br />
float _range;<br />

void Attack();<br />
void Throw;<br />

- **Shields**

sprite2d _sprite;<br />
float _damage;<br />
float _durability;<br />
float _range;<br />

void Throw();<br />
void Attack();<br />

- **Ammo**

sprite2d _sprite;<br />
int _ammo;<br />
enum _ammoType{ Shootgun, Pistol, Riffle, Granades, Electric?}<br />

#More ideas
All weapons are weapons, a weapon can shoot weapons.<br />
f to grab weapon/if all weapon slots are filled you drop current weapon and exchenge it for the new one.<br />
hold f to Throw weapon.<br />
q to drop weapon.<br />
when you dont have more ammo you atomaticlly throw the weapon.<br />
Every weapon has duravility if it drops to 0 the weapon breaks.<br />
If you throw the weapon at an enemy it breaks and deals more damage.<br />
Right click to shoot.<br />
V to melee.<br />
You can melee with any weapon, grenades explote and you die.<br />

