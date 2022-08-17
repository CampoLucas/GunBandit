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
enum _fireMode{ Automatic, ...}<br />
enum _ammoType{ Shootgun, Pistol, Riffle, Granades, Electric}<br />

_ _When the player is out of ammo, automaticaly throws the weapon / if he presses the throw key_ _<br />
void Throw();<br />
void Fire();<br />
void Reload();<br />

- **Granade**

sprite2d _sprite<br />
float _damage;<br />
float _range;<br />
int _ammo;<br />

sprite2d _sprite<br />
void Throw();<br />
void Fire();<br />
void Explode();<br />

- **Melee Attacks**

sprite2d _sprite;<br />
float _damage;<br />
float _range;<br />

sprite2d _sprite;<br />
void Attack();<br />
void Throw;<br />

- **Shields**

sprite2d _sprite;<br />
float _life;<br />
float _durability;<br />

void Throw();<br />

- **Ammo**

sprite2d _sprite;<br />
int _ammo;<br />
enum _ammoType{ Shootgun, Pistol, Riffle, Granades, Electric}<br />




# GDD
