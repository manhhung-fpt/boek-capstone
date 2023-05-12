import { GenreViewModel } from "../Genres/GenreViewModel";

export interface CampaignCommissionsViewModel {
    id?: number;
    campaignId?: number;
    genreId?: number;
    commission?: number;
    genre: GenreViewModel;
}