#include "./game.h"
#include "./car.h"

game::game()
{
    m_cars = new std::vector<car *>();

    

    m_cars->push_back(new car(0, 0));
}

game::~game()
{
    for (car *i : *m_cars)
        delete i;

    delete m_cars;
}

void game::cycle()
{
    
}

void game::draw()
{

}

void game::carCalculate()
{
    
}