import { useAppSelector } from "../app/hooks";
import { Loader } from "../components/Loader";
import { TimeTable } from "../components/Times/TimeTable";
import { TimeModal } from "../components/Times/TimeModal";

export const TimePage = () => {
  const { times, selectedTime, loading, error } = useAppSelector(state => state.trackedTimes);

  return (
    <>
      <div className="block mt-4 mb-4">
          <h1 className="title">Times Page</h1>
      </div>

      {loading && <Loader />}

      {!loading && error && (
        <div className="notification is-danger">
          {error}
        </div>
      )}

      {!loading && !error && times.length === 0 && (
        <div className="notification is-warning">
          No tracked time yet
        </div>
      )}

      {!loading && !error && times.length > 0 && (
        <div className="block">
          <div className="box table-container">
            <TimeTable times={times} />
          </div>
        </div>
      )}
    
      {selectedTime && <TimeModal time={selectedTime} />}
    </>
  )
}