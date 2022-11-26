import type { SortBy, SortDirection } from "@/components/SortDescription";
import type { ListRegisterEntriesItem } from "@/contract/ListRegisterEntriesItem";
import type { SaveRegisterEntryRequest } from "@/contract/SaveRegisterEntryRequest";
import { ApiService } from "./ApiService";

export class RegisterService extends ApiService {

    public add(request: SaveRegisterEntryRequest) {
        return this.put<void>('/Register/Add', request);
    }

    public list(keyword?: string, sortBy?: SortBy, sortDirection?: SortDirection) {
        return this.get<ListRegisterEntriesItem[]>('/Register/List', { keyword, sortBy, sortDirection });
    }

    public remove(id: number) {
        return this.delete<void>('/Register/Remove', { id });
    }
}