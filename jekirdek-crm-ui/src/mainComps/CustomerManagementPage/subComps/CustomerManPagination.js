import { custManStyles } from "../styles/customerManStyle";

//Sayfalama Bileşenimiz
export default function CustomerManPagination(props) {
    //Props Drilling İle Baba Bileşenden propsları Aldık
    const { pageNumber, pageCount, setPageNumber } = props

    //Sayfada İleri Gitme Slice Tektiklencek pageNumber State Sayesinde
    const handleNextPage = () => {
        //Sayfa Sayısının Kontrolü İle Tetiklenme Sağlar Daha Geri Gidemez
        if (pageNumber < pageCount - 1) {
            setPageNumber(pageNumber + 1);
        }
    };

    //Sayfada Geri Gitme Slice Tektiklencek pageNumber State Sayesinde
    const handlePrevPage = () => {
        //Sayfa Sayısının Kontrolü İle Tetiklenme Sağlar Daha İleri Gidemez
        if (pageNumber > 0) {
            setPageNumber(pageNumber - 1);
        }
    };
    return (
        <div style={custManStyles.pagination}>
            <button
                style={pageNumber === 0 ? { ...custManStyles.button, ...custManStyles.disabledButton } : custManStyles.button}
                onMouseEnter={(e) => {
                    if (pageNumber > 0) e.target.style.backgroundColor = "#0056b3";
                }}
                onMouseLeave={(e) => {
                    if (pageNumber > 0) e.target.style.backgroundColor = "#007bff";
                }}
                onClick={handlePrevPage}
                disabled={pageNumber === 0}
            >
                Geri
            </button>
            <button
                style={pageNumber === pageCount - 1 ? { ...custManStyles.button, ...custManStyles.disabledButton } : custManStyles.button}
                onMouseEnter={(e) => {
                    if (pageNumber < pageCount - 1) e.target.style.backgroundColor = "#0056b3";
                }}
                onMouseLeave={(e) => {
                    if (pageNumber < pageCount - 1) e.target.style.backgroundColor = "#007bff";
                }}
                onClick={handleNextPage}
                disabled={pageNumber === pageCount - 1}
            >
                İleri
            </button>
        </div>
    );
}