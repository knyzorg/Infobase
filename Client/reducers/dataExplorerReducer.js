

const initialState = {
    filters: [],
    chartData: {
        xAxis: {},
        yAxis: {},
        points: [],
        source: {},
        organization: {},
        population: {},
        notes: {},
        definition: {},
        dataAvailable: {},
        method: {},
        remarks: {},
        warningCV: null,
        suppressCV: null,
        measureName: {}
    },
    loading: false,
    languageCode: "en-ca"
}

export const initState = (payload) => {
    return { type: "INIT_STATE", payload };
}

export const updateLoadState = (payload) => {
    return { type: "LOAD", payload };
}

export const updateFilters = (payload) => {
    return { type: "UPDATE_FILTERS", payload };
}

export const updateChartData = (payload) => {
    return { type: "UPDATE_DATA", payload };
}


export const dataExplorerReducer = (previousState = initialState, action) => {
    let state = { ...previousState };
    switch (action.type) {
        case "LOAD":
            state.loading = action.payload;
            break;
        case "UPDATE_FILTERS":
            state.filters = action.payload;
            break;
        case "UPDATE_DATA":
            state.chartData = action.payload;
            break;
        case "INIT_STATE":
            return { previousState, ...action.payload };
    }

    return state;
}