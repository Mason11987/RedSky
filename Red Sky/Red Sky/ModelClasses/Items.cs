using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Red_Sky.ModelClasses
{
    public class Items : IEnumerable
    {
        private Item[] _item;

        public Items()
        {

        }

        public void Initialize(RedSkyGame game)
        {
            _item = new Item[13];

            List<string> ItemNames = new List<string>() { "Potion", "Pheonix Down", "Ether", "Mega Potion", "Elixer", "Remedy", "Hi-Potion", "Fire Item" };

            for (int i = 0; i < 8; i++)
            {
                _item[i] = new Item(game);
                _item[i].Name = ItemNames[i];
                _item[i].Usage = ItemUse.Usable;
                _item[i].Targets = ItemTargets.Character;
                _item[i].Description = _item[i].Name + " Description here.";
            }

            _item[3].Targets = ItemTargets.Party;
            
            _item[7].Usage = ItemUse.BattleUsable;


            Equipment Sword = new Equipment(game);
            Sword.Name = "Sword";
            Sword.Description = Sword.Name + " Description here.";
            Sword.HeldBy = BodyPartType.Hand;
            Sword.Weight = 1;
            Sword.AttackDamage = 5;
            Sword.Strength = 3;
            _item[8] = Sword;

            Equipment Axe = new Equipment(game);
            Axe.Name = "Axe";
            Axe.Description = Sword.Name + " Description here.";
            Axe.HeldBy = BodyPartType.Hand;
            Axe.Weight = 3;
            Axe.AttackDamage = 10;
            Axe.Strength = 5;
            _item[9] = Axe;

            Equipment Gloves = new Equipment(game);
            Gloves.Name = "Gloves";
            Gloves.Description = Gloves.Name + " Description here.";
            Gloves.WornOn = BodyPartType.Hand;
            Gloves.Weight = 100;
            Gloves.MagicalAbility = 5;
            Gloves.Vitality = 2;
            _item[10] = Gloves;

            Equipment Helmet = new Equipment(game);
            Helmet.Name = "Helmet";
            Helmet.Description = Helmet.Name + " Description here.";
            Helmet.WornOn = BodyPartType.Head;
            Helmet.Weight = 4;
            _item[11] = Helmet;

            Equipment Shirt = new Equipment(game);
            Shirt.Name = "Shirt";
            Shirt.Description = Shirt.Name + " Description here.";
            Shirt.WornOn = BodyPartType.Body;
            Shirt.Weight = 2;
            _item[12] = Shirt;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return new ItemEnum(_item);
        }

        public int getIDOf(string itemName)
        {
            for (int i = 0; i < _item.Length; i++)
			{
                if (_item[i].Name == itemName)
                {
                    return i;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        public Item this[string Index]
        {
            get
            {
                foreach (Item item in _item)
                {
                    if (item.Name == Index)
                        return item;
                }
                return null;
            }
            set
            {
                for (int i = 0; i < _item.Length; i++)
			    {
                    if (_item[i].Name == Index)
                    {
                        _item[i] = value;
                        return;
                    }
                }
            }
        }

        public Item this[int Index]
        {
            get
            {
                if (Index < 0 || Index >= _item.Length)
                {
                    return null;
                }
                else
                    return _item[Index];
            }
            set
            {
                if (Index < 0 || Index >= _item.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                    _item[Index] = value;
            }
        }

    }

    public class ItemEnum : IEnumerator
    {
        public Item[] _item;

        // Enumerators are positioned before the first element 
        // until the first MoveNext() call. 
        int position = -1;

        public ItemEnum(Item[] list)
        {
            _item = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _item.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Item Current
        {
            get
            {
                try
                {
                    return _item[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}