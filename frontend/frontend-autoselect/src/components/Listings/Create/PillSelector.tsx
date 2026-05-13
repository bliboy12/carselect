
interface PillselectorProps {
    options: string[],
    selected: string | null,
    onSelect: (value: string) => void
}

const PillSelector = ({ options, selected, onSelect }: PillselectorProps) => {
    return (
        <div className="flex flex-wrap gap-3">
            {options.map((o) =>
                <button key={o} type="button" onClick={() => onSelect(o)} className={`rounded-full px-5 py-2 text-sm font-medium border transition-all cursor-pointer ${
                        o === selected
                            ? "border-blue-500 bg-blue-500/20 text-blue-400 shadow-sm shadow-blue-500/20"
                            : "border-gray-700 bg-gray-800/50 text-gray-400 hover:border-gray-500 hover:text-gray-300"
                    }`}>
                    {o}
                </button>
            )}
        </div>
    )
}

export default PillSelector;