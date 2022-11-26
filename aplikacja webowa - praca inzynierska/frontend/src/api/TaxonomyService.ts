import type { SaveTaxonomyCodeRequest } from "@/contract/SaveTaxonomyCodeRequest";
import type { SearchTaxonomyItem } from "@/contract/SearchTaxonomyItem";
import type { TaxonomyItemDetails } from "@/contract/TaxonomyItemDetails";
import { ApiService } from "./ApiService";

export class TaxonomyService extends ApiService {

    public addCode(request: SaveTaxonomyCodeRequest) {
        return this.put<void>('/Taxonomy/AddCode', request);
    }

    public details(id: string) {
        return this.get<TaxonomyItemDetails | undefined>('/Taxonomy/Details', { id });
    }

    public find(code: string) {
        return this.get<SearchTaxonomyItem[]>('/Taxonomyy/Find', { code });
    }
}