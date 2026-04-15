import myCar from "../../assets/car.jpg"


const BrandPanel = () => {
    return (
    <div className="relative flex flex-col justify-center gap-6 p-12 bg-gray-900 overflow-hidden min-h-screen">
      
      {/* Background car image */}
      <img
        src={myCar}
        alt=""
        className="absolute inset-0 w-full h-full object-cover opacity-20"
      />

      {/* Gradient overlay so text stays readable */}
      <div className="absolute inset-0 from-gray-900 via-gray-900/60 to-transparent" />

      {/* Content sits on top */}
      <div className="relative z-10">
        <p className="text-blue-500 font-semibold text-sm">● AutoSelect</p>
        <div className="mt-6">
          <h2 className="text-4xl font-bold text-white leading-tight">
            Koop en verkoop <span className="text-blue-400">slimmer</span>
          </h2>
          <p className="text-gray-400 mt-4 text-sm leading-relaxed">
            Toegang tot duizenden listings. Betaal veilig via Stripe. Sla
            je favorieten op en bekijk je volledige aankoophistorie.
          </p>
        </div>
      </div>

    </div>
  );
}

export default BrandPanel;