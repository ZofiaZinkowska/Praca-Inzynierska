export type SortBy = 'date'|'name';
export type SortDirection = 'asc'|'desc';
export interface SortDescription 
{
    by: SortBy;
    direction: SortDirection;
};