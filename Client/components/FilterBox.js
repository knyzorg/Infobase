// @flow

import * as React from 'react';
import { Filter } from "./Filter";
import { i18n } from '../Translator';
import type { Action, FilterData, ChartData, MultilangText } from "../types";

type FilterBoxProps = {
    loading: boolean,
    filters: FilterData[],
    updateLoadState: boolean => Action,
    updateFilters: FilterData[] => Action,
    updateChartData: ChartData => Action,
    prompt: MultilangText
}

export class FilterBox extends React.Component<FilterBoxProps> {

    async selectFilter(selected: number): Promise<boolean> {
        this.props.updateLoadState(true);

        history.pushState(null, document.title, `?index=${selected}`);

        let request = await fetch(window.location.toString(), {
            method: 'POST'
        })
        try {
            let response: Response = await request;
            let r = await response.json();

            this.props.updateFilters(r.filters);
            this.props.updateChartData(r.chartData);
            this.props.updateLoadState(false);
            return true;
        } catch (e) {
            return false;
        }
    }
    render() {
        return (
            <div className="col-md-3 padding-15 ">
                <span className="text-info">{i18n(this.props.prompt)}:</span>
                <div className="form-group-md">
                    {
                        this.props.filters.map((filter, i) =>
                            <Filter
                                key={i}
                                id={"drop"+i}
                                items={filter.items}
                                selected={filter.selected}
                                onSelect={selected => this.selectFilter(selected)}
                                loading={this.props.loading}
                            />)
                    }
                </div>
            </div>
        );
    }
}