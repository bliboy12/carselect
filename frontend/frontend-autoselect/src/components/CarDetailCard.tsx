import { useState } from "react";
import type { CarInfo } from "../types";

interface CarDetail {
    Car: CarInfo
}


const CarDetailCard = ({Car}: CarDetail) => {
    const [price] = useState(() => Math.floor(Math.random() * 10000));

    const carWithPrice = {
        ...Car,
        price
    }

    return (
        <div>
            <div>
                <div>
                    <img/>
                </div> 
                <div>
                    <h3 className="text-2xl font-bold">{carWithPrice.brand} {carWithPrice.model}</h3>
                    <h4 className="text-lg font-semibold">{carWithPrice.buildYear} {carWithPrice.trim}</h4>
                </div>
                <div>
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                </div>
            </div>
            <div>
                <div>
                    <h3 className="text-2xl font-bold">{carWithPrice.price}</h3>
                    <button>Buy Now</button>
                    <button>Favorite Now</button>
                </div>
                <div>
                    <p>Aangeboden door</p>
                    <h3 className="text-2xl font-bold">BOB MARLEY</h3>
                    <p>Lid sinds 2019 - 12 verkopen - ⭐ 4.9</p>
                </div>
            </div>
        </div>
    );
}

export default CarDetailCard;