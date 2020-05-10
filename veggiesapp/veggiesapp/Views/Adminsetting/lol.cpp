#include<iostream>
#include<vector>

using namespace std;

int main()
{
	int arr[] = {1,3,4,5,8};
	int threshold = 4;

	vector<int> ans;

	ans.push_back(arr[0]);

	for (int i = 1; i < sizeof(arr); i++)
	{
		if(arr[i]-arr[0]>=threshold)
		{
			ans.push_back(arr[i]);
			break;
		}
		else
		{
			i++;
			ans.push_back(arr[i]);
		}
	}

	cout<<ans.size();
}