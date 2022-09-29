#include <iostream>
#include <vector>
#include "./car.h"


class game
{
public:
    game();
    ~game();

private:
    std::vector<car *> *m_cars;

    void cycle();
    void draw();

    void carCalculate();


};