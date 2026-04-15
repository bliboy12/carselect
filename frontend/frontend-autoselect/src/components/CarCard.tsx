import type { CarInfo } from "../types";


interface CarDetail {
    Car: CarInfo
}


const CarCard = ({Car}: CarDetail) => {

    return (
        <div>
            <div>
                <img/>
            </div>
            <div>
                <h3 className="text-2xl font-bold">{Car.brand} {Car.model}</h3>
                <h4 className="text-lg font-light">{Car.trim} {Car.buildYear}</h4>
            </div>
        </div>
    )
}

export default CarCard;