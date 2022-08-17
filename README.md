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

_ _When the player is out of ammo, automaticaly throws the weapon / if he presses the throw key_ _<br />
void Throw();<br />
void Fire();<br />
void Reload();<br />
void Attack();<br />

- **Granade**

sprite2d _sprite<br />
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

sprite2d _sprite;<br />
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



